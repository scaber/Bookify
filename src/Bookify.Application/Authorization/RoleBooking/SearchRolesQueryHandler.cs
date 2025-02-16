using System.Data;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Authorization.RoleBooking;

internal sealed class SearchRolesQueryHandler
    : IQueryHandler<SearchRolesQuery, IReadOnlyList<RoleResponse>>
{ 

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchRolesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<RoleResponse>>> Handle(SearchRolesQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<RoleResponse>();
        }

        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            select id,"name" ,description ,validity_date from  roles r 
            """;

        IEnumerable<RoleResponse> apartments = await connection
            .QueryAsync<RoleResponse>(
                sql,
                 
                new
                {
                    request.StartDate,
                    request.EndDate
                } );

        return apartments.ToList();
    }
}
