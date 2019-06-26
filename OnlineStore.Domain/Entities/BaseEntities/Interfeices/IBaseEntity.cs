using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.Base.Interfeices
{
    /// <summary>
    ///  Интерфейс базовой сущности
    /// </summary>
    public interface IBaseEntity
    {
        int id { get; set; }
    }
}
