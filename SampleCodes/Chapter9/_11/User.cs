using System;

namespace _11
{
    public class User
    {
        private readonly UserId id;
        private UserName name;

        // 생성자 메서드가 있다는 것만 알 수 있다
        public User(UserId id, UserName name)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.id = id;
            this.name = name;
        }

        public UserId Id => id;
        public UserName Name => name;

        public void ChangeName(UserName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.name = name;
        }
    }
}
