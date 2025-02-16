using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Authorization;

namespace Bookify.Application.Authorization.RoleBooking;

internal sealed class RoleComandHandler : ICommandHandler<RoleComand, Guid>
{
    private readonly IRoleRepository _roleRepository;

    private readonly IUnitOfWork _unitOfWork;


    public RoleComandHandler(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    } 

    public async Task<Result<Guid>> Handle(RoleComand request, CancellationToken cancellationToken)
    {

        Role? role = await _roleRepository.GetByNameAsync(request.Name, cancellationToken);
        if (role is not null)
        {
            return Result.Failure<Guid>(new Error(
                     "Role.Exist",
                     "The role is already exist"));
        }
        try
        {
            var roleData = Role.CreateRole(
                request.Name,
                request.Description,
                request.ValidityDate);
            _roleRepository.Add(roleData);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return roleData.Id;
        }
        catch (Exception)
        {

            return Result.Failure<Guid>(new Error(
                     "Role.Adding",
                     "The role adding error"));

        }

    }
}
