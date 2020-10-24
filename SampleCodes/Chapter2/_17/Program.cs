using System;

namespace _17
{
    class Program
    {
        static void Main(string[] args)
        {
            var nameA = new FullName("masanobu", "naruse");
            var nameB = new FullName("john", "smith");

            var compareResult = nameA.Equals(nameB);
            Console.WriteLine(compareResult);

            // 연산자 오버라이드를 활용할 수도 있다
            var compareResult2 = nameA == nameB;
            Console.WriteLine(compareResult2);
        }
    }
}
