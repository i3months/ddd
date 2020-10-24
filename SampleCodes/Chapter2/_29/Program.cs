namespace _29
{
    class Program
    {
        static void Main(string[] args)
        {
            var myMoney = new Money(1000, "KRW");
            var allowance = new Money(3000, "KRW");
            var result = myMoney.Add(allowance);
        }
    }
}
