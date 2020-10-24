namespace _14
{
    public class UserData
    {
        public UserData(User source)
        {
            Id = source.Id.Value;
            Name = source.Name.Value;
            MailAddress = source.MailAddress.Value; // 속성에 대입
        }

        public string Id { get; }
        public string Name { get; }
        public string MailAddress { get; } // 추가된 속성 
    }
}
