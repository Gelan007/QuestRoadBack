﻿using QuestRoadBack.Contracts;
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
        public async Task<User> GetUser(int id)
        {
            var query = "SELECT * FROM User WHERE user_id = @id";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
                return user;
            }
        }
        public async Task CreateUser(User user)
        {
            var query = "INSERT INTO User (name,password,email,phone) VALUES (@name,@password,@email,@phone)";
            var parameters = new DynamicParameters();
            parameters.Add("name", user.name, DbType.String);
            parameters.Add("password", user.password, DbType.String);
            parameters.Add("email", user.email, DbType.String);
            parameters.Add("phone", user.phone, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateUser(int id, User user)
        {
            var query = "UPDATE Customer SET name = @name, password = @password, email = @email, phone = @phone WHERE user_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("name", user.name, DbType.String);
            parameters.Add("password", user.password, DbType.String);
            parameters.Add("email", user.email, DbType.String);
            parameters.Add("phone", user.phone, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteUser(int id)
        {
            var query = "DELETE FROM User WHERE user_id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
