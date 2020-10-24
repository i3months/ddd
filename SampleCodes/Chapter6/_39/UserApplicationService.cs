namespace _39
{
    public class UserApplicationService
    {
        private readonly IUserRepository userRepository;
        private readonly UserService userService;

        public UserApplicationService(IUserRepository userRepository, UserService userService)
        {
            this.userRepository = userRepository;
            this.userService = userService;
        }

        public void Register(UserRegisterCommand command)
        {
            var user = new User(
                new UserName(command.Name)
            );

            if (userService.Exists(user))
            {
                throw new CanNotRegisterUserException(user, "이미 등록된 사용자임");
            }

            userRepository.Save(user);
        }
    }
}
