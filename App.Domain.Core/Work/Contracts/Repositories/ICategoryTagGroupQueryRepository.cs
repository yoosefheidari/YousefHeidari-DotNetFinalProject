﻿using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ICategoryTagGroupQueryRepository
    {
        Task<List<CategoryTagGroup>> GetAll(CancellationToken cancellationToken);
        Task<CategoryTagGroup> Get(int id, CancellationToken cancellationToken);
    }
}