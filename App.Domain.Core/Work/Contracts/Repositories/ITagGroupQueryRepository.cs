using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ITagGroupQueryRepository
    {
        Task<List<TagGroupDTO>> GetAll(CancellationToken cancellationToken);
        Task<TagGroupDTO> Get(int id, CancellationToken cancellationToken);
        Task<TagGroupDTO> Get(string name, CancellationToken cancellationToken);
    }
}
