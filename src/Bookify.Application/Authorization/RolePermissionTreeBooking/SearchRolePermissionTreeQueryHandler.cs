using Bookify.Api.Controllers.Authorization;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Authorization.RolePermissionTreeBooking;
using Bookify.Domain.Abstractions;
using Dapper;
using System.Data;

namespace Bookify.Application.Apartments.SearchApartments;

internal sealed class SearchRolePermissionTreeQueryHandler
    : IQueryHandler<SearchRolePermissionTreeQuery, IReadOnlyList<RolePermissionTreeResponse>>
{ 
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchRolePermissionTreeQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<RolePermissionTreeResponse>>> Handle(SearchRolePermissionTreeQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<RolePermissionTreeResponse>();
        }

        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                 
                p.seviye AS Level,
                r.name AS RoleName,
                COALESCE(rp.read, TRUE) AS Read,
                r.id AS RoleId,
                COALESCE(rp.delete, TRUE) AS Delete,
                COALESCE(rp.write, TRUE) AS Write,
                p.name AS PermissionName,
                p.id AS PermissionId,
                COALESCE(rp.id, '00000000-0000-0000-0000-000000000000') AS Id,  
                 '' AS ParentName
            FROM permissions p   
            CROSS JOIN roles r  
            LEFT JOIN role_permissions rp ON rp.permission_id = p.id AND rp.role_id = r.id
            ORDER BY r.id; 
            """;

        IEnumerable<RolePermissionTreeResponse> rolePermissionTreeResponses = await connection
            .QueryAsync<RolePermissionTreeResponse>(
                sql, new
                {
                    request.StartDate,
                    request.EndDate
                });

        var returnVal = new List<RolePermissionTreeResponse>();
        foreach (var rolId in rolePermissionTreeResponses.Select(x => x.RoleId).Distinct().ToList())
        {
            returnVal.AddRange(FillHierarchy(rolePermissionTreeResponses.ToList(), 1, rolePermissionTreeResponses.Max(x => x.Level), "", rolId));
        }

        return returnVal;
    }
    private static IEnumerable<RolePermissionTreeResponse> FillHierarchy(List<RolePermissionTreeResponse> query, int currentIndex, int max, string parentName,
          Guid? rolId)
    {
        if (currentIndex > max)
            return new List<RolePermissionTreeResponse>();
        var kirilimlar = (from q in query
                          where q.Level == currentIndex && q.RoleId == rolId
                          select new RolePermissionTreeResponse
                          {

                              Checked = q.Id.ToString() == "00000000-0000-0000-0000-000000000000" ? false : true,
                              Level = q.Level,
                              RoleName = q.RoleName,
                              Read = q.Read,
                              RoleId = q.RoleId,
                              Delete = q.Delete,
                              Write = q.Write,
                              PermissionName = q.PermissionName,
                              PermissionId = q.PermissionId,
                              ParentName = parentName,
                              SubAuthorities = FillHierarchy(query, currentIndex + 1, max, q.PermissionName, rolId)
                                  .Where(y => y.PermissionName.StartsWith(q.PermissionName + "."))
                                  .ToList()
                          }).ToList();
        return kirilimlar;
    }
}
