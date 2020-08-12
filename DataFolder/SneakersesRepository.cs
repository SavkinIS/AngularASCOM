using Angular.Interfaces;
using Angular.Models;
using Angular.StoreDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.DataFolder
{
    /// <summary>
    /// Репозиторий Sneakers с реализацией логики взаимодействия с БД
    /// </summary>
    public class SneakersesRepository : IRepository<Sneakers>
    {
        StoreContext data;
        public SneakersesRepository(StoreContext data)
        {
            this.data = data;
        }

        public void Create(Sneakers item)
        {
            data.Sneakerses.Add(item);
            data.SaveChanges();
        }

        public void Delete(int id)
        {
            var sneakers = data.Sneakerses.Find(id);
            data.Sneakerses.Remove(sneakers);
            data.SaveChanges();
        }

        public Sneakers GetDataItem(int id)
        {
            return data.Sneakerses.Find(id);
        }

        public IEnumerable<Sneakers> GetDataList()
        {
            return data.Sneakerses.ToList();

        }

        public void Update(Sneakers item)
        {
            data.Entry(item).State = EntityState.Modified;
            data.SaveChanges();
        }

        public void Save()
        {
            data.SaveChanges();
        }
    }
}
