namespace _16
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new InMemoryUserRepository();

            var user = new User(new UserName("John"));

            // 여기서 인스턴스를 바로 리포지토리에 저장하면
            userRepository.Save(user);
            // 인스턴스에 대한 조작이 리포지토리에 저장된 객체에도 영향을 미친다
            user.ChangeUserName(new UserName("john"));
        }
    }
}
