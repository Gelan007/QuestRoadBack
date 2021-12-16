using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Contracts
{
    public interface IQuestRepository
    {
        public Task<IEnumerable<Quest>> GetQuests();
        public Task<Quest> GetQuest(int id);
        public Task CreateQuest(Quest quest);
        public Task UpdateQuest(int id, Quest quest);
        public Task DeleteQuest(int id);
        public Task<IEnumerable<Quest>> GetMostPopularQuestsAsync();
    }
}
