using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.Base.Interfeices
{
    /// <summary>
    /// Интерфейс упорядочеваемой сущность
    /// </summary>
    public interface IOrderedEntity
    {
        int Order { get; set; }
    }
}
