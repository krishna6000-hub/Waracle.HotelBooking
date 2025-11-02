namespace Waracle.HotelBooking.Domain.Interfaces;

public interface IDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}