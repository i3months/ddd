using System;

namespace _14
{
    public class User
    {
        // 외부로 공개하지 않아도 된다
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

        // 팩토리 역할을 하는 메서드
        public Circle CreateCircle(CircleName circleName)
        {
            return new Circle(
                id,
                circleName
            );
        }
    }
}
