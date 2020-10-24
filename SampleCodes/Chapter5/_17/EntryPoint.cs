using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _17
{
    class EntryPoint
    {
        public static void Main(string[] args)
        {
            var userRepository = new InMemoryUserRepository();
            var program = new Program(userRepository);
            program.CreateUser("john");

            // 리포지토리에서 데이터를 꺼내 확인한다
            var head = userRepository.Store.Values.First();
            Assert.AreEqual("john", head.Name);
        }
    }
}
