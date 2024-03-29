﻿using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ISuggestCommandRepository
    {
        Task<int> Add(SuggestDTO suggest, CancellationToken cancellationToken);
        Task Update(SuggestDTO suggest, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
