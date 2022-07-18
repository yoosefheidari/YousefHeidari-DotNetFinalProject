
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public int Price { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
