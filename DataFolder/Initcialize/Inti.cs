using Angular.Auth;
using Angular.Models;
using Angular.StoreDB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.DataFolder.Initcialize
{
    /// <summary>
    /// Класс Реализует первичное заполнени БД для тестов
    /// </summary>
    public class Inti
    {
        
            static readonly string adm = "admin";
            /// <summary>
            /// First Initialize DataBase
            /// </summary>
            /// <param name="userManager"> Клас управления пользователями</param>
            /// <param name="roleManager">Класс управления ролями</param>
            /// <param name="context">Контекст БД </param>
            /// <returns></returns>
            public static async Task InitializeAsync(UserManager<User> userManager,
                                                     RoleManager<IdentityRole> roleManager,
                                                     StoreContext context)
            {
            
                //Create first admin
                var admin = new User { UserName = "admin", Email = "admin@gmail.com" };
                string password = "Admin000!";

                //провкрка ролей, в случае отсутствия добавление
                if (await roleManager.FindByNameAsync(adm) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(adm));
                }

                //проверка на наличие пользователя с ником Админ, в случае отсутствие выполняет добавление еге 
                if (await userManager.FindByNameAsync(admin.UserName) == null)
                {

                    IdentityResult result = await userManager.CreateAsync(admin, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, adm);
                        context.SaveChanges();
                    }
                }
                if (!context.Sneakerses.Any())
                {
                    List<Sneakers> list = new List<Sneakers>
                {
                    new Sneakers{ Company  = "Nike", Color = "Белый", Name = "AirMax", Price = 8999 },
                    new Sneakers{ Company  = "Nike", Color = "Чёрный", Name = "AirMax", Price = 8999 },
                    new Sneakers{ Company  = "Adidas", Color = "Чёрный", Name = "NX200", Price = 9999 },
                    new Sneakers{ Company  = "Adidas", Color = "Белый", Name = "NX200", Price = 9999 },
                    new Sneakers{ Company  = "Reabok", Color = "Белый", Name = "Leather", Price = 7999 }
                };

                    using (var trans = context.Database.BeginTransaction())
                    {
                        foreach (var s in list) context.Sneakerses.Add(s);

                        context.SaveChanges();
                        trans.Commit();
                    }
                }
                
            }
        
    }
}
