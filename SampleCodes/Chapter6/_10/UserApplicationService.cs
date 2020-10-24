namespace _10
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

        public void Register(string name, string mailAddress)
        {
            var user = new User(
                new UserName(name),
                new MailAddress(mailAddress)
            );
            if (userService.Exists(user))
            {
                throw new CanNotRegisterUserException(user, "이미 등록된 사용자임");
            }

            userRepository.Save(user);
        }

        public UserData Get(string userId)
        {
            var targetId = new UserId(userId);
            var user = userRepository.Find(targetId);

            // var userData = new UserData(user.Id.Value, user.Name.Value);
            // 생성자 메서드의 인자가 늘어남
            var userData = new UserData(user.Id.Value, user.Name.Value, user.MailAddress.Value);
            return userData;
        }
    }
}
