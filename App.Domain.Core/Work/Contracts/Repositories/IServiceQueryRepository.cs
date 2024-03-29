﻿using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IServiceQueryRepository
    {
        Task<List<ServiceDTO>> GetAll(int id, CancellationToken cancellationToken);
        Task<ServiceDTO> Get(int id, CancellationToken cancellationToken);
        Task<ServiceDTO> Get(string name, CancellationToken cancellationToken);
        Task<List<PhysicalFileDTO>> GetAllFiles(int ServiceId, CancellationToken cancellationToken);
    }
}
