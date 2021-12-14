using QuestRoadBack.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using QuestRoadBack.Models;
using QuestRoadBack.Contex;

namespace QuestRoadBack.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {

            var query = "SELECT * FROM [User]";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }

        }
        public async Task Registration(string email, string phone, string password, string name, UserRole role, int company_id)
        {
            var query = "Insert into [User] (email,phone,password,name,role, company_id) VALUES (@email,@phone,@password,@name,@role,@company_id)";

            var parameters = new DynamicParameters();
            parameters.Add("email", email, DbType.String);
            parameters.Add("phone", phone, DbType.String);
            parameters.Add("password", BCrypt.Net.BCrypt.HashPassword(password), DbType.String);
            parameters.Add("name", name, DbType.String);        
            parameters.Add("role", role, DbType.Int64);
            parameters.Add("company_id", company_id, DbType.Int64);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<User> GetUser(int id)
        {
            var query = "SELECT * FROM [User] WHERE user_id = @id";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
                return user;
            }
        }
        public async Task CreateUser(User user)
        {
            var query = "INSERT INTO [User] (email, phone, password, name, role, company_id) VALUES (@email, @phone, @password, @name, @role, @company_id)";
            var parameters = new DynamicParameters();
            parameters.Add("email", user.Email, DbType.String);
            parameters.Add("phone", user.Phone, DbType.String);
            parameters.Add("password", user.Password, DbType.String);
            parameters.Add("name", user.Name, DbType.String);
            parameters.Add("role", user.Role, DbType.Int64);
            parameters.Add("company_id", user.Company_id, DbType.Int64);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateUser(int id, User user)
        {
            var query = "UPDATE [User] SET email = @email, phone = @phone, password = @password, name = @name, role = @role, company_id = @company_id WHERE user_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("email", user.Email, DbType.String);
            parameters.Add("phone", user.Phone, DbType.String);
            parameters.Add("password", user.Password, DbType.String);
            parameters.Add("name", user.Name, DbType.String);
            parameters.Add("role", user.Role, DbType.Int64);
            parameters.Add("company_id", user.Company_id, DbType.Int64);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteUser(int id)
        {
            var query = "DELETE FROM [User] WHERE user_id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }


        public async Task<User> IsItAnExistingMail(string email)
        {
            string query = "Select * from [User] where  email = @email ";
            using (var connection = _context.CreateConnection())
            {
                var cust = await connection.QueryFirstOrDefaultAsync<User>(query, new { email });
                return cust;
            }
        }

        public async Task<User> GetUserByParams(string email, string phone, string password, string name, UserRole role)
        {
            string query = "Select * from [User] where email = @email and phone = @phone and password = @password and name = @name and role = @role";
            var parameters = new DynamicParameters();
            parameters.Add("email", email, DbType.String);
            parameters.Add("phone", phone, DbType.String);
            parameters.Add("password", password, DbType.String);
            parameters.Add("name", name, DbType.String);
            parameters.Add("role", role, DbType.Int64);
            using (var connection = _context.CreateConnection())
            {
                var cust = await connection.QueryFirstOrDefaultAsync<User>(query, parameters);
                return cust;
            }
        }


        public async Task<User> Login(string email, string password)
        {

            var query = "select * FROM [User] WHERE email = @email";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { email });
                bool IsUser = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (IsUser == true)
                {
                    return user;
                }
                return null;

            }

        }
    }
}
