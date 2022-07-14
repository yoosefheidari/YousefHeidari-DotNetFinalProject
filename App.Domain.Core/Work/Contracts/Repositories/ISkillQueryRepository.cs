using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ISkillQueryRepository
    {
        Task<List<SkillDTO>> GetAll(CancellationToken cancellationToken);
        Task<SkillDTO> Get(int id, CancellationToken cancellationToken);
        Task<SkillDTO> Get(string name, CancellationToken cancellationToken);
    }
}
