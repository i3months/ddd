using System;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            var checkId = new UserId("check");
            var checkName = new UserName("checker");
            var checkObject = new User(checkId, checkName);

            var userId = new UserId("id");
            var userName = new UserName("nrs");
            var user = new User(userId, userName);

            // 사용자명 중복 확인용 객체에 중복 여부를 문의함
            var duplicateCheckResult = checkObject.Exists(user);
            Console.WriteLine(duplicateCheckResult);
        }
    }
}
