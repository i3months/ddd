namespace _15
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new InMemoryUserRepository();

            // 객체를 복원할 때 깊은 복사를 하지 않으면
            var user = userRepository.Find(new UserName("John"));
            // 복원된 객체에 대한 조작이 리포지토리에 저장된 객체에도 영향을 미친다
            user.ChangeUserName(new UserName("john"));
        }
    }
}
