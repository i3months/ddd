using System;

namespace _37
{
    class Program
    {
        static void Main(string[] args)
        {
            var userName = "me";

            if (userName.Length >= 3)
            {
                // 유효한 값이므로 처리를 계속한다
            }
            else
            {
                throw new Exception("유효하지 않은 값");
            }
        }
    }
}
