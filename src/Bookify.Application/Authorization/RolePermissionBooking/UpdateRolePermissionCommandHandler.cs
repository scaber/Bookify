using Bookify.Application.Abstractions.Messaging;
 using Bookify.Domain.Abstractions;
using Bookify.Domain.Authorization; 

namespace Bookify.Application.Authorization.RolePermissionBooking;

internal sealed class UpdateRolePermissionCommandHandler : ICommandHandler<UpdateRolePermissionCommand, bool>
{
    private readonly IRolePermissionRepository _repository;
 
    public UpdateRolePermissionCommandHandler(IRolePermissionRepository repository )
    {
        _repository = repository;
     }

    public async Task<Result<bool>> Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var existingRolePermission = await _repository.GetByIdAsync(request.RoleId, request.PermissionId);
        if (existingRolePermission == null)
        {
            return false;  
        }
        // Güncelleme işlemi
        existingRolePermission.Read = request.Read;
        existingRolePermission.Write = request.Write;
        existingRolePermission.Delete = request.Delete;

        await _repository.UpdateAsync(existingRolePermission); 
        return true;
    }

    
}
