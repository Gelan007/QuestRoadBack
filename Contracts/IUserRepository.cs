using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(int id);
        public Task CreateUser(User user);
        public Task UpdateUser(int id, User user);
        public Task DeleteUser(int id);
        public Task<User> IsItAnExistingMail(string email);
        public Task Registration(string email, string phone, string password, string name, UserRole role, int company_id);
        public Task<User> GetUserByParams(string email, string phone, string password, string name, UserRole role);
        public Task<User> Login(string email, string password);

        public Task<User> GetPhoneByIdAsync(int id);
    }
}
