﻿using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ISkillTagGroupCommandRepository
    {
        Task<int> Add(SkillTagGroup skillTagGroup, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
