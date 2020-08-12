using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular.Auth;
using Angular.DataFolder.Initcialize;
using Angular.StoreDB;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Angular
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            //Реализация первичного заполнения БД
            var init = BuildWebHost(args);
            using (var scope = init.Services.CreateScope())
            {
                var s = scope.ServiceProvider;
                var c = s.GetRequiredService<StoreContext>();
                var userManager = s.GetRequiredService<UserManager<User>>();
                var rolesManager = s.GetRequiredService<RoleManager<IdentityRole>>();
                await Inti.InitializeAsync(userManager, rolesManager, c);
            }
            init.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
               WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .Build();
    }
}
