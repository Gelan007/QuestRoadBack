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
    public class MemberRepository : IMemberRepository
    {
        private readonly DapperContext _context;

        public MemberRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Member>> GetMembers()
        {

            var query = "SELECT * FROM [Member]";
            using (var connection = _context.CreateConnection())
            {
                var members = await connection.QueryAsync<Member>(query);
                return members.ToList();
            }

        }
        public async Task CreateMember(Member member)
        {
            var query = "INSERT INTO [Member] (user_id, team_id, when_assigned) VALUES (@user_id,@team_id,@when_assigned)";
            var parameters = new DynamicParameters();
            parameters.Add("user_id", member.User_id, DbType.Int32);
            parameters.Add("team_id", member.Team_id, DbType.Int32);
            parameters.Add("when_assigned", member.When_assigned, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteMember(int id)
        {
            var query = "DELETE FROM [Member] WHERE member_id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<int> GetCountOfUsersByTeamIdAsync(int teamId)
        {
            var query = "SELECT COUNT(user_id) FROM Member WHERE team_id = @teamId";
            using(var connection = _context.CreateConnection())
            {
                var member = await connection.QuerySingleOrDefaultAsync<int>(query, new { teamId });
                return member;
            }
        }
        //----
        public Task CreateMemberAsync(int user_id, int team_id, DateTime when_assigned)
        {
            throw new NotImplementedException();
        }
    }
}
