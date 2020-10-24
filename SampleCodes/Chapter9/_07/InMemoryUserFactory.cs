using System;

namespace _07
{
    class InMemoryUserFactory : IUserFactory
    {
        // 마지막으로 발행된 식별자
        private int currentId;

        public User Create(UserName name)
        {
            // 사용자를 생성할 때마다 1씩 증가
            currentId++;

            return new User(
                new UserId(currentId.ToString()),
                name
            );
        }
    }
}
