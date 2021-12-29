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
    public class TeamRepository: ITeamRepository
    {
        private readonly DapperContext _context;

        public TeamRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Team>> GetTeams()
        {

            var query = "SELECT * FROM [Team]";
            using (var connection = _context.CreateConnection())
            {
                var teams = await connection.QueryAsync<Team>(query);
                return teams.ToList();
            }

        }
        public async Task<Team> GetTeam(int id)
        {
            var query = "SELECT * FROM [Team] WHERE team_id = @id";
            using (var connection = _context.CreateConnection())
            {
                var team = await connection.QuerySingleOrDefaultAsync<Team>(query, new { id });
                return team;
            }
        }
        public async Task CreateTeam(Team team)
        {
            var query = "INSERT INTO [Team] (name, count, phone) VALUES (@name,@count,@phone)";
            var parameters = new DynamicParameters();
            parameters.Add("name", team.Name, DbType.String);
            parameters.Add("count", team.Count, DbType.Int64);
            parameters.Add("phone", team.Phone, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateTeam(int id, Team team)
        {
            var query = "UPDATE [Team] SET name = @name, count = @count, phone = @phone";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("name", team.Name, DbType.String);
            parameters.Add("count", team.Count, DbType.Int32);
            parameters.Add("phone", team.Phone, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteTeam(int id)
        {
            var query = "DELETE FROM [Team] WHERE team_id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<int> GetTeamIdByPhoneAsync(string phone)
        {
            var query = "SELECT * FROM Team WHERE phone = @phone";
            using(var connection = _context.CreateConnection())
            {
                var team = await connection.QuerySingleOrDefaultAsync<Team>(query, new { phone});
                return team.Team_id;
            }
        }
        public async Task CreateTeamFromBookingAsync(string name, int count, string phone)
        {
            var query = "INSERT INTO [Team] (name, count,phone ) VALUES (@name,@count,@phone)";
            var parameters = new DynamicParameters();
            parameters.Add("name", name, DbType.String);
            parameters.Add("count", count, DbType.Int64);
            parameters.Add("phone", phone, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        // получить команду по имени и телефону    если что изменить на динам параметры
        
        public async Task<Team> GetTeamByPhoneAndNameAsync(string name, string phone)
        {
            var query = "SELECT * FROM Team WHERE name = @name and phone = @phone";
            using (var connection = _context.CreateConnection())
            {
                var team = await connection.QuerySingleOrDefaultAsync<Team>(query, new { name, phone });
                return team;
            }
        }
    }
}
