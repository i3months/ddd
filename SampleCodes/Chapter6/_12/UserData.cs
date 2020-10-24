namespace _12
{
    public class UserData
    {
        public UserData(User source) // 도메인 객체를 인자로 받음
        {
            Id = source.Id.Value;
            Name = source.Name.Value;
        }

        public string Id { get; }
        public string Name { get; }
    }
}
