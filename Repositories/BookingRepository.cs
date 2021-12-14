using Dapper;
using QuestRoadBack.Contex;
using QuestRoadBack.Contracts;
using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Repositories
{
    public class BookingRepository: IBookingRepository
    {
        private readonly DapperContext _context;

        public BookingRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Booking>> GetBookings()
        {

            var query = "SELECT * FROM [Booking]";
            using (var connection = _context.CreateConnection())
            {
                var bookings = await connection.QueryAsync<Booking>(query);
                return bookings.ToList();
            }

        }
        public async Task<Booking> GetBooking(int id)
        {
            var query = "SELECT * FROM [Booking] WHERE booking_id = @id";
            using (var connection = _context.CreateConnection())
            {
                var booking = await connection.QuerySingleOrDefaultAsync<Booking>(query, new { id });
                return booking;
            }
        }
        public async Task CreateBooking( int quest_id, int team_id, int price, DateTime time, string descriptio)
        {
            var query = "INSERT INTO [Booking] (quest_id,team_id,price,time,description) VALUES (@quest_id,@team_id,@price@time,@description)";
            var parameters = new DynamicParameters();
            parameters.Add("quest_id", quest_id, DbType.Int64);
            parameters.Add("team_id", team_id, DbType.Int64);
            //Добавил price
            parameters.Add("price", price, DbType.Int64);
            parameters.Add("time", time, DbType.DateTime);
            parameters.Add("descriptio", descriptio, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateBooking(int id, Booking booking)
        {
            var query = "UPDATE [Booking] SET quest_id = @quest_id, team_id = @team_id, time = @time, description = @description WHERE booking_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("quest_id", booking.Quest_id, DbType.Int64);
            parameters.Add("team_id", booking.Team_id, DbType.Int64);
            parameters.Add("time", booking.Time, DbType.DateTime);
            parameters.Add("description", booking.Description, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteBooking(int id)
        {
            var query = "DELETE FROM [Booking] WHERE booking_id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
        //придумать касательно коефов
        public async Task UpdateBookingPriceAsync(int team_id, double coef)
        {
            var query = "UPDATE [Booking] SET price = price * @coef WHERE team_id = @team_id";
            var parameters = new DynamicParameters();
            
           
            parameters.Add("team_id", team_id, DbType.Int64);
            

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
