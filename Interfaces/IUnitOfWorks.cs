using Angular.DataFolder;
using Angular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.Interfaces
{
    interface IUnitOfWorks
    {
        public SneakersesRepository Sneakers { get; }
    }
}
