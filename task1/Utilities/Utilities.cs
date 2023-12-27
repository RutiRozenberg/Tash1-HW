using Microsoft.Extensions.DependencyInjection;
using task1.Interface;

namespace Utilities{
    using TaskService.Services;
    public static  class Utilities{
        public static void AddTask(this IServiceCollection services)
        {
            services.AddSingleton<ITaskInterface,TaskService>();
        }
    }
    

}
