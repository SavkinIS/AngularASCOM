using Angular.Auth;
using Angular.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.StoreDB
{
    /// <summary>
    /// Класс Контекста БД
    /// </summary>
    public class StoreContext :IdentityDbContext<User>
    {

       public DbSet<Sneakers> Sneakerses { get; set; }

        public StoreContext(DbContextOptions options) : base(options) { }

        public StoreContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ///Изменить строку подключения
            optionsBuilder.UseSqlServer(
                 @"Server=(LocalDB)\MSSQLLocalDB;
                     Database=AngularStoreDB.mdf;
                    Trusted_Connection=True;"
                );
        }
    }
}
