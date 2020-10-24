namespace _29
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Exists(User user)
        {
            // 사용자 중복 기준을 사용자명에서 이메일 주소로 변경
            // var duplicatedUser = userRepository.Find(user.Name);
            var duplicatedUser = userRepository.Find(user.MailAddress);

            return duplicatedUser != null;
        }
    }
}
