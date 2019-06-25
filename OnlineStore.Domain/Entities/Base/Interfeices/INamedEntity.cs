using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.Base.Interfeices
{
    /// <summary>
    /// Интерфейс именованной сущности
    /// </summary>
    public interface INamedEntity
    {
        string Name { get; set; }
    }
}
