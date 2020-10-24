namespace _16
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new InMemoryUserRepository();
            // 2번째 인자로 IFooRepository의 구현체가 전달되지 않았으므로 컴파일 에러 발생
            // var userApplicationService = new UserApplicationService(userRepository);
        }
    }
}
