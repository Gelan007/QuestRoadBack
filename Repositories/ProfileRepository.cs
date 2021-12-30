using Dapper;
using QuestRoadBack.Contex;
using QuestRoadBack.Contracts;
using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DapperContext _dapperContext;

        public ProfileRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<User> GetUserInfoAsync(int id)
        {
            var query = "select * from [User] where user_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
                return user;
            }
        }

        public async Task<IEnumerable<Booking>> GetUsersBookingsAsync(int id)
        {
            var query = "select Booking.booking_id, Booking.quest_id, Booking.team_id, Booking.time, Booking.description, Booking.price from Booking join Team on Booking.team_id = Team.team_id  join Member on Team.team_id = Member.team_id Where Member.user_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var bookings = await connection.QueryAsync<Booking>(query, new { id });
                return bookings.ToList();
            }

        }
    }
}
