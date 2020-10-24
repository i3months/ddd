using System;
using System.Collections.Generic;
using _14.EFInfrastructure.Contexts;
using _14.SnsDomain.Models.Users;

namespace _14.EFInfrastructure.Persistence.Users
{
    public class EFUserRepository : IUserRepository
    {
        private readonly MyDbContext context;

        public EFUserRepository(MyDbContext context)
        {
            this.context = context;
        }

        public User Find(UserId id)
        {
            throw new NotImplementedException();
        }

        public User Find(UserName name)
        {
            throw new NotImplementedException();
        }

        public List<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Save(User user)
        {
            // 노티피케이션 객체를 전달했다가 다시 회수해 내부 데이터를 입수한다
            var userDataModelBuilder = new UserDataModelBuilder();
            user.Notify(userDataModelBuilder);

            // 전달받은 내부 데이터로 데이터 모델을 생성
            var userDataModel = userDataModelBuilder.Build();

            // 데이터 모델을 ORM에 전달한다
            context.Users.Add(userDataModel);
            context.SaveChanges();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}
