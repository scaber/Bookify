using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Menus;

public sealed record MenusQuery() : IQuery<IReadOnlyList<MenuResponse>>;