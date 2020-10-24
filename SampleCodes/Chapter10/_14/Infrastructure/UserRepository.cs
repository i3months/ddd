﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using _14.Domain.Models.Users;

namespace _14.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly Dictionary<UserId, User> cloned = new Dictionary<UserId, User>();

        private readonly SqlConnection connection;
        private readonly SqlTransaction transaction;

        public UserRepository(SqlConnection connection, SqlTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public User Find(UserId id)
        {
            User user;
            // 사용자 정보를 받아오는 코드
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM users WHERE id = @id";
                command.Parameters.Add(new SqlParameter("@id", id.Value));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var name = reader["name"] as string;
                        user = new User(
                            id,
                            new UserName(name)
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            // 받아온 사용자를 저장
            var cloneInstance = Clone(user);
            cloned.Add(id, cloneInstance);
            return user;
        }

        public User Find(UserName name)
        {
            User user;
            // 사용자 정보를 받아오는 코드 
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM users WHERE name = @name";
                command.Parameters.Add(new SqlParameter("@name", name.Value));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var id = reader["id"] as string;
                        user = new User(
                            new UserId(id),
                            name
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            // 받아온 사용자를 저장
            var cloneInstance = Clone(user);
            cloned.Add(user.Id, cloneInstance);
            return user;
        }

        public void Save(User user)
        {
            if (cloned.TryGetValue(user.Id, out var recent))
            {
                SaveUpdate(recent, user);
            }
            else
            {
                SaveNew(user);
            }
        }

        private User Clone(User user)
        {
            return new User(user.Id, user.Name);
        }

        private void SaveNew(User user)
        {
            // UPSERT 쿼리를 실행
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = @"
 MERGE INTO users
 USING (
   SELECT @id AS id, @name AS name
 ) AS data
 ON users.id = data.id
 WHEN MATCHED THEN
   UPDATE SET name = data.name
 WHEN NOT MATCHED THEN
   INSERT (id, name)
   VALUES (data.id, data.name);
";
                command.Parameters.Add(new SqlParameter("@id", user.Id.Value));
                command.Parameters.Add(new SqlParameter("@name", user.Name.Value));
                command.ExecuteNonQuery();
            }
        }

        private void SaveUpdate(User recent, User latest)
        {
            // 변경된 항목에 따라 UPDATE 쿼리를 만들어 실행
        }

        public void Delete(User user)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = "DELETE FROM users WHERE id = @id";
                command.Parameters.Add(new SqlParameter("@id", user.Id.Value));
                command.ExecuteNonQuery();
            }
        }
    }
}
