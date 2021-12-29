using QuestRoadBack.Contex;
using QuestRoadBack.Contracts;
using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace QuestRoadBack.Repositories
{
    public class HelpRepository : IHelpRepository
    {
        private readonly DapperContext _context;
        public HelpRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<User> GetPhoneByIdAsync(int id)
        {
            var query = "SELECT * FROM [User] WHERE user_id = @id";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
                return user;
            }
        }

        public async Task<UserRole> IsAdminAsync(int id)
        {
            var query = "Select * from [User] Where user_id = @id";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
                return user.Role;
            }
        }
    }
}
