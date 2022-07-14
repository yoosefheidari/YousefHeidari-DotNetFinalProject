using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ITagQueryRepository
    {
        Task<List<TagDTO>> GetAll(CancellationToken cancellationToken);
        Task<TagDTO> Get(int id, CancellationToken cancellationToken);
        Task<TagDTO> Get(string name, CancellationToken cancellationToken);
    }
}
