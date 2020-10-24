using System;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            var fullName = "naruse masanobu";
            var tokens = fullName.Split(' '); // ["naruse", "masanobu"]와 같은 배열이 만들어짐
            var lastName = tokens[0];
            Console.WriteLine(lastName); // naruse가 출력됨
        }
    }
}
