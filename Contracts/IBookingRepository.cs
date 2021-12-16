using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Contracts
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetBookings();
        public Task<Booking> GetBooking(int id);
        public Task CreateBooking( int quest_id, int team_id, int price, DateTime date, string desc);
        public Task UpdateBooking(int id, Booking booking);
        public Task DeleteBooking(int id);
        public Task UpdateBookingPriceAsync(int team_id, double coef);
    }
}
