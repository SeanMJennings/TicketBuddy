using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Infrastructure.Bookings")]
[assembly: InternalsVisibleTo("Testing.Unit.Bookings")]
// using this to allow EF Core access to aggregate entities for navigation whilst keeping them internal to the domain