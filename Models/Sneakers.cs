using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.Models
{
    /// <summary>
    /// Описание модели предмета
    /// </summary>
    public class Sneakers
    {
        public int Id { get; set; }
        /// <summary>
        /// название
        /// </summary>
        public string Name { get; set; }    
        /// <summary>
        /// производитель
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// цена
        /// </summary>
        public int Price { get; set; }  
        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Цвет
        /// </summary>
        public string Color { get; set; }
    }
}
