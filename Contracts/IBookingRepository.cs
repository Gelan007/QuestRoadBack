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
        public Task CreateBooking(Booking booking);
        public Task UpdateBooking(int id, Booking booking);
        public Task DeleteBooking(int id);
    }
}
