using System;
using Microsoft.Extensions.Options;

namespace UniVolunteerApi.Moq
{
    public class TestOptionsMonitor<T> : IOptionsMonitor<T>
    {
        public TestOptionsMonitor(T inner)
        {
            this.inner = inner;
        }


        private T inner { get; }

        public T CurrentValue => inner;

        public T Get(string name)
        {
            throw new NotImplementedException();
        }

        public IDisposable OnChange(Action<T, string> listener)
        {
            throw new NotImplementedException();
        }
    }
}
