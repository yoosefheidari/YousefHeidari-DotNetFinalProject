using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DisplayOrder { get; set; }
        public int? ParentCategoryId { get; set; }
        public decimal? BasePrice { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
