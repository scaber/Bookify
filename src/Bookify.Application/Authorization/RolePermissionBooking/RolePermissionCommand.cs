﻿using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Authorization.RolePermissionBooking;

public sealed record UpdateRolePermissionCommand(
        Guid RoleId,
        Guid PermissionId,
        bool Read,
        bool Write,
        bool Delete
    ) : ICommand<bool>;
