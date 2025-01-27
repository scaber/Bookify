using Bookify.Application.Abstractions.Caching;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Application.Bookings.GetBooking;

namespace Bookify.Application.Menu;
 
public sealed record MenusQuery() : IQuery<IReadOnlyList<MenuResponse>>;