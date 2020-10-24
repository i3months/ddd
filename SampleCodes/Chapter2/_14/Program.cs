using System;

namespace _14
{
    class Program
    {
        static void Main(string[] args)
        {
            var nameA = new FullName("masanobu", "naruse");
            var nameB = new FullName("masanobu", "naruse");

            // 두 인스턴스를 비교
            Console.WriteLine(nameA.Equals(nameB)); // 인스턴스를 구성하는 속성이 같으므로 true
        }
    }
}
