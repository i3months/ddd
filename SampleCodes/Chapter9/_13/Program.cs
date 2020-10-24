using System;
using System.Collections.Generic;
using System.Text;

namespace _13
{
    class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(new UserId("test-id"), new UserName("test-name"));

            var circle = new Circle(
                user.Id, // 게터를 통해 사용자 ID를 받아옴
                new CircleName("my circle")
            );
        }
    }
}
