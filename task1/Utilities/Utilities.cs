using Microsoft.Extensions.DependencyInjection;
using task1.Interface;


namespace Utilities{

    
    using MyTask.Models;
    using TaskService.Services;
    using User.Models;
    using UserService.Services;


    public static  class Utilities{
        public static void AddTask(this IServiceCollection services)
        {
            services.AddSingleton<ITaskInterface,TaskService>();
        }

        public static void AddUser(this IServiceCollection services)
        {
            services.AddSingleton<IUserInterface,UserService>();
        }
    }
}
