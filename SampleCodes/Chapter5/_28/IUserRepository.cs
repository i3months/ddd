using System.Collections.Generic;

namespace _28
{
    interface IUserRepository
    {
        void Save(User user);
        void Delete(User user);
        User Find(UserId id);
        User Find(UserName name);
        // 오버로딩을 지원하지 않는 언어라면 이름을 바꿔가며 배리에이션을 만든다
        // User FindByUserName(UserName name);
    }
}
