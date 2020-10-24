using System;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            var userId = new UserId("id");
            var userName = new UserName("nrs");
            var user = new User(userId, userName);

            // 새로 만든 객체에 중복 여부를 묻는 상황이 됨
            var duplicateCheckResult = user.Exists(user);
            Console.WriteLine(duplicateCheckResult); // true? false?
        }
    }
}
