using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

using PracticeApp.Models;

namespace PracticeApp.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True";

        public IEnumerable<User> Get()
        {
            using var conn = new SqlConnection(_connectionString);

            return conn.Query<User>("SELECT * FROM AppUser");
        }

        public User Get(int id)
        {
            // TODO: сделать отдельным селектом по ИД
            return Get().Where(t => t.Id == id).FirstOrDefault();
        }

        public IEnumerable<UserRole> GetUserRoles()
        {
            // TODO: Сделать получение ролей с помощью селекта из БД
            return new List<UserRole>()
            {
                new UserRole() { Id = 1, Name = "admin" }
            };
        }

        public void Add(User user)
        {
            using var conn = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Login", user.Login);
            parameters.Add("@RoleId", user.RoleId);

            conn.Execute(
                "INSERT INTO AppUser (Login, RoleId)" +
                "VALUES (@Login, @RoleId)", parameters);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
