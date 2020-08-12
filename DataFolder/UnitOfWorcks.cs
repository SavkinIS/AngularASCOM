using Angular.Interfaces;
using Angular.Models;
using Angular.StoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.DataFolder
{
    public class UnitOfWorcks : IUnitOfWorks
    {
        StoreContext data = new StoreContext();
        SneakersesRepository sneakers;
        public SneakersesRepository Sneakers
        {
            get
            {
                if (sneakers == null)
                {
                    sneakers = new SneakersesRepository(data);
                }
                return sneakers;
            }
        }
    }
}
