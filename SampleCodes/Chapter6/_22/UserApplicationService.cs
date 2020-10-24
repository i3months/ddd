namespace _22
{
    public class UserApplicationService
    {
        private readonly IUserRepository userRepository;
        
        public UserApplicationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Register(string name, string mailAddress)
        {
            // 사용자명 중복 여부를 확인하는 코드
            var userName = new UserName(name);
            var duplicatedUser = userRepository.Find(userName);
            if (duplicatedUser != null)
            {
                throw new CanNotRegisterUserException(userName, "이미 등록된 사용자임");
            }

            var user = new User(
                userName
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
                // 사용자명 중복 여부를 확인하는 코드 
                var newUserName = new UserName(name);
                var duplicatedUser = userRepository.Find(newUserName);
                if (duplicatedUser != null)
                {
                    throw new CanNotRegisterUserException(user, "이미 등록된 사용자임");
                }
                user.ChangeName(newUserName);
            }

            var mailAddress = command.MailAddress;
            if (mailAddress != null)
            {
                var newMailAddress = new MailAddress(mailAddress);
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
