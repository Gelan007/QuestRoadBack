using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Contracts
{
    public interface IProfileRepository
    {
        public Task<User> GetUserInfoAsync(int id);
        public Task<IEnumerable<Booking>> GetUsersBookingsAsync(int id);
    }
}
