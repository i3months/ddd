using System;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            var userService = new UserService();

            var userId = new UserId("id");
            var userName = new UserName("naruse");
            var user = new User(userId, userName);

            // 사용자명 중복 확인용 객체에 중복 여부를 문의함
            var duplicateCheckResult = userService.Exists(user);
            Console.WriteLine(duplicateCheckResult);
        }
    }
}
