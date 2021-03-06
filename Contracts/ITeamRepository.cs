using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Contracts
{
    public interface ITeamRepository
    {
        public Task<IEnumerable<Team>> GetTeams();
        public Task<Team> GetTeam(int id);
        public Task CreateTeam(Team team);
        public Task CreateTeamFromBookingAsync(string name, int count, string phone);
        public Task UpdateTeam(int id, Team team);
        public Task DeleteTeam(int id);
        public Task<int> GetTeamIdByPhoneAsync(string phone);
        public Task<Team> GetTeamByPhoneAndNameAsync(string name, string phone);
        
    }
}
