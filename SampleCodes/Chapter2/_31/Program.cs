namespace _31
{
    class Program
    {
        static void Main(string[] args)
        {
            var jpy = new Money(1000, "KRW");
            var usd = new Money(10, "USD");

            var result = jpy.Add(usd); // 예외 발생
        }
    }
}
