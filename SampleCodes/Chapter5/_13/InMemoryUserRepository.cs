using System;
using System.Collections.Generic;
using System.Linq;

namespace _13
{
    class InMemoryUserRepository : IUserRepository
    {
        // 테스트케이스에 따라 데이터를 확인해야 하는 경우도 있다
        // 확인을 위해 외부에서 접근할 수 있게 public으로 둔다
        public Dictionary<UserId, User> Store { get; } = new Dictionary<UserId, User>();

        public User Find(UserName userName)
        {
            var target = Store.Values
                .FirstOrDefault(user => userName.Equals(user.Name));
            if (target != null)
            {
                // 인스턴스를 직접 반환하지 않고 깊은 복사한 사본을 반환
                return Clone(target);
            }
            else
            {
                return null;
            }
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
