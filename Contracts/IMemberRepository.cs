using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Contracts
{
    public interface IMemberRepository
    {
        public Task<IEnumerable<Member>> GetMembers();
        public Task CreateMember(Member member);
        public Task DeleteMember(int user_id, int team_id);
        public Task<int> GetCountOfUsersByTeamIdAsync(int id);
        public Task CreateMemberAsync(int user_id, int team_id, DateTime when_assigned);
    }
}
