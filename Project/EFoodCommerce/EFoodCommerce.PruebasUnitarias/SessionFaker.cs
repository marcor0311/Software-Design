using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EFoodCommerce.PruebasUnitarias
{
    public class SessionFaker
    {
        public static IHttpContextAccessor FakeHttpContextAccessor()
        {
            var httpContext = new DefaultHttpContext();
            var contextAccessor = new HttpContextAccessor { HttpContext = httpContext };
            var session = new FakeSession();

            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton<IHttpContextAccessor>(contextAccessor)
                .BuildServiceProvider();

            contextAccessor.HttpContext.Session = session;

            return contextAccessor;
        }

        public static void SetFakeHttpContext(Controller controller)
        {
            var contextAccessor = FakeHttpContextAccessor();
            controller.ControllerContext.HttpContext = contextAccessor.HttpContext!;
        }
    }

    public class FakeSession : ISession
    {
        static readonly Dictionary<string, byte[]> data = [];
        
        public bool IsAvailable => throw new NotImplementedException();

        public string Id => throw new NotImplementedException();

        public IEnumerable<string> Keys => throw new NotImplementedException();

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value)
        {
            data[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            return data.TryGetValue(key, out value!);
        }
    }
}
