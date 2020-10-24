namespace _18
{
    public class UserUpdateCommand
    {
        public UserUpdateCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
        /// <summary> 지정된 값으로 해당 항목이 수정됨 </summary>
        public string Name { get; set; }
        /// <summary> 지정된 값으로 해당 항목이 수정됨 </summary>
        public string MailAddress { get; set; }
    }
}
