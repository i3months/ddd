namespace _27
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
            // 도메인 서비스를 통해 중복 확인
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

            if (user == null)
            {
                return null;
            }

            var userData = new UserData(user);
            return userData;
        }

        public void Update(UserUpdateCommand command)
        {
            var targetId = new UserId(command.Id);
            var user = userRepository.Find(targetId);

            if (user == null)
            {
                throw new UserNotFoundException(targetId);
            }

            var name = command.Name;
            if (name != null)
            {
                // 사용자명의 중복은 허용된다
                var newUserName = new UserName(name);
                user.ChangeName(newUserName);
            }

            var mailAddress = command.MailAddress;
            if (mailAddress != null)
            {
                // 대신 이메일 주소의 중복이 금지된다
                var newMailAddress = new MailAddress(mailAddress);
                var duplicatedUser = userRepository.Find(newMailAddress);
                if (duplicatedUser != null)
                {
                    throw new CanNotRegisterUserException(newMailAddress);
                }
                user.ChangeMailAddress(newMailAddress);
            }

            userRepository.Save(user);
        }

        public void Delete(UserDeleteCommand command)
        {
            var targetId = new UserId(command.Id);
            var user = userRepository.Find(targetId);

            if (user == null)
            {
                // 탈퇴 대상 사용자가 발견되지 않았다면 탈퇴 처리 성공으로 간주한다
                return;
            }

            userRepository.Delete(user);
        }
    }
}
