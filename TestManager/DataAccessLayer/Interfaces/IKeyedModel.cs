using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс сущности базы данных
    /// </summary>
    interface IKeyedModel
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        int Id { get; set; }
    }
}
