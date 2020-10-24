using _14.EFInfrastructure.Persistence.DataModels;
using _14.SnsDomain.Models.Users;

namespace _14.EFInfrastructure.Persistence.Users
{
    public class UserDataModelBuilder : IUserNotification
    {
        // 전달된 데이터는 인스턴스 변수로 저장된다
        private UserId id;
        private UserName name;
        public void Id(UserId id)
        {
            this.id = id;
        }
        public void Name(UserName name)
        {
            this.name = name;
        }
        // 전달받은 데이터로 데이터 모델을 생성하는 메서드
        public UserDataModel Build()
        {
            return new UserDataModel
            {
                Id = id.Value,
                Name = name.Value
            };
        }
    }
}
