namespace _26
{
    public class UserApplicationService
    {
        private readonly IUserRepository userRepository;

        public UserApplicationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Register(string name, string rawMailAddress)
        {
            // 이메일 주소의 중복을 확인하도록 변경되었다
            var mailAddress = new MailAddress(rawMailAddress);
            var duplicatedUser = userRepository.Find(mailAddress);
            if (duplicatedUser != null)
            {
                throw new CanNotRegisterUserException(mailAddress);
            }

            var userName = new UserName(name);
            var user = new User(
                userName,
                mailAddress
            );

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
