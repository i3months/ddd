namespace _17
{
    public class UserApplicationService
    {
        private readonly IUserRepository userRepository;
        
        // 생성자 메서드를 통해 의존 관계를 주입함
        public UserApplicationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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
    }
}
