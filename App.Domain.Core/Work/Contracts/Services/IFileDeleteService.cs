using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Services
{
    public interface IFileDeleteService
    {
        Task DeletePhysicalFile(string fileName, CancellationToken cancellationToken);
    }
}
