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
    public class QuestRepository: IQuestRepository
    {
        private readonly DapperContext _context;

        public QuestRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Quest>> GetQuests()
        {

            var query = "SELECT * FROM [Quest]";
            using (var connection = _context.CreateConnection())
            {
                var quests = await connection.QueryAsync<Quest>(query);
                return quests.ToList();
            }

        }
        public async Task<Quest> GetQuest(int id)
        {
            var query = "SELECT * FROM Quest WHERE quest_id = @id";
            using (var connection = _context.CreateConnection())
            {
                var quest = await connection.QuerySingleOrDefaultAsync<Quest>(query, new { id });
                return quest;
            }
        }
        public async Task CreateQuest(Quest quest)
        {
            var query = "INSERT INTO [Quest] (name,description,difficulty_level,city, adress, category, actors, company_id, max_count_users, price) VALUES (@name,@description,@difficulty_level,@city, @adress, @category, @actors, @company_id, @max_count_users, @price)";
            var parameters = new DynamicParameters();
            parameters.Add("name", quest.Name, DbType.String);
            parameters.Add("description", quest.Description, DbType.String);
            parameters.Add("difficulty_level", quest.Difficulty_level, DbType.String);
            parameters.Add("city", quest.City, DbType.String);
            parameters.Add("adress", quest.Adress, DbType.String);
            parameters.Add("category", quest.Category, DbType.String);
            parameters.Add("actors", quest.Actors, DbType.String);
            parameters.Add("company_id", quest.Company_id, DbType.Int64);
            parameters.Add("max_count_users", quest.Max_count_users, DbType.String);
            parameters.Add("price", quest.Price, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateQuest(int id, Quest quest)
        {
            var query = "UPDATE [Quest] SET name = @name, description = @description, difficulty_level = @difficulty_level,city = @city, adress= @adress, category = @category, actors = @actors, company_id = @company_id, max_count_users = @max_count_users, price = @price WHERE quest_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);
            parameters.Add("name", quest.Name, DbType.String);
            parameters.Add("description", quest.Description, DbType.String);
            parameters.Add("difficulty_level", quest.Difficulty_level, DbType.String);
            parameters.Add("city", quest.City, DbType.String);
            parameters.Add("adress", quest.Adress, DbType.String);
            parameters.Add("category", quest.Category, DbType.String);
            parameters.Add("actors", quest.Actors, DbType.String);
            parameters.Add("company_id", quest.Company_id, DbType.Int64);
            parameters.Add("max_count_users", quest.Max_count_users, DbType.String);
            parameters.Add("price", quest.Price, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteQuest(int id)
        {
            var query = "DELETE FROM [Quest] WHERE quest_id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Quest>> GetMostPopularQuestsAsync()
        {
            var query = "select Quest.quest_id, Quest.name, Quest.description, Quest.difficulty_level, Quest.city, Quest.adress, Quest.category, Quest.actors, Quest.company_id, Quest.max_count_users, Quest.price from Quest Left join Booking on Quest.quest_id = Booking.quest_id group by Quest.quest_id, Quest.name, Quest.description, Quest.difficulty_level, Quest.city, Quest.adress, Quest.category, Quest.actors, Quest.company_id, Quest.max_count_users, Quest.price having count(Booking.booking_id) >= 0 order by count(Booking.booking_id) desc";
            using (var connection = _context.CreateConnection())
            {
                var quests = await connection.QueryAsync<Quest>(query);
                return quests.ToList();
            }
        }
    }
}
