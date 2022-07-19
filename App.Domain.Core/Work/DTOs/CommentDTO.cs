using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ServiceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
