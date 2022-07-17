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
        public string Title { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public int? Rating { get; set; }
        public bool? DoesRecomended { get; set; }
        public bool IsDeleted { get; set; }
    }
}
