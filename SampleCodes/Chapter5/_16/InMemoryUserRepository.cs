using System.Collections.Generic;

namespace _16
{
    class InMemoryUserRepository : IUserRepository
    {
        // 테스트케이스에 따라 데이터를 확인해야 하는 경우도 있다
        // 확인을 위해 외부에서 접근할 수 있게 public으로 둔다
        public Dictionary<UserId, User> Store { get; } = new Dictionary<UserId, User>();

        public User Find(UserName name)
        {
            foreach (var elem in Store.Values)
            {
                if (elem.Name.Equals(name))
                {
                    return Clone(elem);
                }
            }

            return null;
        }

        public void Save(User user)
        {
            // 저장 시에도 깊은 복사를 수행
            Store[user.Id] = Clone(user);
        }

        // 깊은 복사를 담당하는 메서드
        private User Clone(User user)
        {
            return new User(user.Id, user.Name);
        }
    }
}
