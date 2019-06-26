using OnlineStore.Domain.Entities.Base.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Entities.ProductsEntities
{
    /// <summary>
    /// Класс для обработки файлов
    /// </summary>
    [Table("Files")]
    public class FileModel : NamedEntity
    {
        public string Path { get; set; }
    }

}
