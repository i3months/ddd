using System;

namespace _13.SnsDomain.Models.Users
{
    public class User
    {
        // 인스턴스 변수는 모두 비공개
        private readonly UserId id;
        private UserName name;

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

        public void Notify(IUserNotification note)
        {
            // 내부 데이터를 전달
            note.Id(id);
            note.Name(name);
        }
    }
}
