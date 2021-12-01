using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task CreateCompany(Company company);
        public Task UpdateCompany(int id, Company company);
        public Task DeleteCompany(int id);
    }
}
