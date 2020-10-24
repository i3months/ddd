namespace _43
{
    class Program
    {
        private User CreateUser(UserName name)
        {
            var user = new User();
            // user.Id = name; // 컴파일 에러 발생
            return user;
        }
    }
}
