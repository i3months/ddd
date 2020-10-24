using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _21
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            var myContext = MyDbContext.Create();

            var userRepository = new EFUserRepository(myContext);
            var program = new Program(userRepository);
            program.CreateUser("smith");

            // 리포지토리에서 데이터를 꺼내 확인한다
            var head = myContext.Users.First();
            Assert.AreEqual("smith", head.Name);
        }
    }
}
