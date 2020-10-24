using System;

namespace Core.Model.Users
{
    public class User
    {
        private UserName name;

        // UserId를 그대로 식별자로 사용 중
        public User(UserId id, UserName name)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));

            Id = id;
            this.name = name;
        }

        public UserId Id { get; }

        // 이젠 UserName이 식별자인데 식별자를 변경한다
        public void ChangeName(UserName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            this.name = name;
        }
    }
}
