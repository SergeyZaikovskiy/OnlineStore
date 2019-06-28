using OnlineStore.Domain.Entities.Base.Interfeices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Entities.Base.Classes
{
    /// <summary>
    /// Шаблон класса базовой сущности
    /// </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]// Указание на то, что свойство является первичным ключём таблицы
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// Требование для БД устанавливать значение данного свойства при добавлении записи в таблицу   
        public int id { get; set; }
    }
}
