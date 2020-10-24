using System;

namespace _03
{
    class MyProgram
    {
        public void Main(User user, UserChangeNameRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentException("요청에 포함된 Name이 null이거나 빈 문자열임");
            }

            user.ChangeName(request.Name);
        }
    }
}
