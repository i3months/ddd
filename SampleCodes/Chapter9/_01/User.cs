using System;

namespace _01
{
    public class User
    {
        private readonly UserId id;
        private UserName name;

        // 사용자를 최초 생성할 떄 실행되는 생성자 메서드
        public User(UserName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            // 식별자로 GUID를 사용한다
            id = new UserId(Guid.NewGuid().ToString());
            this.name = name;
        }

        // 사용자 객체를 복원할 때 실행되는 생성자 메서드
        public User(UserId id, UserName name)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.id = id;
            this.name = name;
        }

        public void ChangeName(UserName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.name = name;
        }
    }
}
