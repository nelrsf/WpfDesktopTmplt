using Microsoft.Extensions.DependencyInjection;

namespace iPlanner
{
    public static class AppServices
    {
        private static IServiceProvider? _serviceProvider;

        public static IServiceProvider? ServiceProvider
        {
            get => _serviceProvider;
            set => _serviceProvider = value;
        }


        public static T GetService<T>() where T : class
        {
            if (ServiceProvider == null)
            {
                throw new InvalidOperationException("ServiceProvider is not initialized");
            }
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
