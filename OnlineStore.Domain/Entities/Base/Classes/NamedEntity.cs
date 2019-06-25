using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.Base.Classes
{
    /// <summary>
    /// Шаблон класса именованной сущности
    /// </summary>
    public abstract class NamedEntity:BaseEntity
    {
        public string Name { get; set; }
    }
}
