using System;

namespace _17
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

    namespace AnotherOne
    {
        // 생성자 메서드를 통해 이름 혹은 이메일 주소가 지정되지 않았음을 나타낼 수도 있다
        public class UserUpdateCommand
        {
            public UserUpdateCommand(string id, string name = null, string mailAddress = null)
            {
                Id = id;
                Name = name;
                MailAddress = mailAddress;
            }

            public string Id { get; }
            public string Name { get; } // 이런 경우 세터가 필요 없다
            public string MailAddress { get; } // 이런 경우 세터가 필요 없다
        }
    }
}
