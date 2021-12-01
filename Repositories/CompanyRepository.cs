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
    public class CompanyRepository: ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Company>> GetCompanies()
        {

            var query = "SELECT * FROM Company";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }

        }
        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT * FROM [Company] WHERE company_id = @id";
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
                return company;
            }
        }
        public async Task CreateCompany(Company company)
        {
            var query = "INSERT INTO [Company] (name,company_check,email,phone, company_account, is_comfirmed, adress) VALUES (@name,@company_check,@email,@phone, @company_account, @is_comfirmed, @adress)";
            var parameters = new DynamicParameters();
            parameters.Add("name", company.Name, DbType.String);
            parameters.Add("company_check", company.Company_check, DbType.String);
            parameters.Add("email", company.Email, DbType.String);
            parameters.Add("phone", company.Phone, DbType.String);
            parameters.Add("company_account", company.Company_account, DbType.String);
            parameters.Add("is_comfirmed", company.Is_comfirmed, DbType.Boolean);
            parameters.Add("adress", company.Adress, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateCompany(int id, Company company)
        {
            var query = "UPDATE [Company] SET name = @name, company_check = @company_check,email= @email,phone = @phone, company_account = @company_account, is_comfirmed = @is_comfirmed, adress= @adress WHERE company_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);
            parameters.Add("name", company.Name, DbType.String);
            parameters.Add("company_check", company.Company_check, DbType.String);
            parameters.Add("email", company.Email, DbType.String);
            parameters.Add("phone", company.Phone, DbType.String);
            parameters.Add("company_account", company.Company_account, DbType.String);
            parameters.Add("is_comfirmed", company.Is_comfirmed, DbType.Boolean);
            parameters.Add("adress", company.Adress, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM [Company] WHERE company_id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
