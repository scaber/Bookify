using System.Data;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Bookings.GetBooking;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;
using MediatR;

namespace Bookify.Application.Menu;

internal sealed class MenusQueryHandler
    : IQueryHandler<MenusQuery, IReadOnlyList<MenuResponse>>
{
    private readonly IUserContext _userContext; 

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public MenusQueryHandler(ISqlConnectionFactory sqlConnectionFactory,IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<Result<IReadOnlyList<MenuResponse>>> Handle(MenusQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT DISTINCT m.* 
            FROM menu m
            WHERE m.permission_id IN (
            SELECT ra.permission_id 
            FROM "user-roles"  ur
            INNER JOIN roles    r ON ur.role_id  = r.Id
            INNER JOIN role_permissions    ra ON r.Id = ra.role_id 
            WHERE ur.user_id  = @userId

            UNION

            SELECT up.permission_id  
            FROM user_permission up 
            WHERE up.user_id =  @userId);
            """;

        IEnumerable<MenuResponse> menuResponses = await connection
            .QueryAsync<MenuResponse>(sql,new
            {
                userId
            });

       

        return menuResponses.ToList();
    }

     
}
