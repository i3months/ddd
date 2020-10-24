using System;
using System.Configuration;
using System.Data.SqlClient;

namespace _02
{
    public class User
    {
        private readonly UserId id;
        private UserName name;

        public User(UserName name)
        {
            string seqId;
            // 데이터베이스 접속 설정에서 커넥션을 설정
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                // 번호 매기기용 테이블을 이용해 번호를 매김
                command.CommandText = "SELECT seq = (NEXT VALUE FOR UserSeq)";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var rawSeqId = reader["seq"];
                        seqId = rawSeqId.ToString();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }

        public User(UserId id, UserName name)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.id = id;
            this.name = name;
        }

        public void ChangeName(UserName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.name = name;
        }
    }
}
