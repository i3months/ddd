using System;

namespace _23
{
    class LastName
    {
        private readonly string value;

        public LastName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("최소 1글자 이상이어야 함", nameof(value));

            this.value = value;
        }
    }
}
