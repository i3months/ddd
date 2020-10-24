using System;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            var fullName = "john smith";
            var tokens = fullName.Split(' '); // ["john", "smith"]와 같은 배열이 만들어짐
            var lastName = tokens[0];
            Console.WriteLine(lastName); // john이 출력됨
        }
    }
}
