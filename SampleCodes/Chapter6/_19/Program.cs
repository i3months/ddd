namespace _19
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new InMemoryUserRepository();
            var userService = new UserService(repository);
            var userApplicationService = new UserApplicationService(repository, userService);
            
            var id = "test-id";
            var user = new User(new UserId(id), new UserName("test-user"));
            repository.Save(user);

            // 사용자명만 변경함
            var updateNameCommand = new UserUpdateCommand(id)
            {
                Name = "john"
            };
            userApplicationService.Update(updateNameCommand);

            // 이메일 주소만 변경함
            var updateMailAddressCommand = new UserUpdateCommand(id)
            {
                MailAddress = "xxxx@example.com"
            };
            userApplicationService.Update(updateMailAddressCommand);
        }
    }
}
