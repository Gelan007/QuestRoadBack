using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Contracts
{
    public interface IHelpRepository
    {
        public Task<User> GetPhoneByIdAsync(int id);
        public Task<UserRole> IsAdminAsync(int id);
    }
}
