using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseData.DTOs
{
    public class PhysicalFileDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int? ExpertId { get; set; }
        public int? OrderId { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
