using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.User
{
    public class UserFileCommandRepository : IUserFileCommandRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UserFileCommandRepository> _logger;

        public UserFileCommandRepository(AppDbContext appDbContext, ILogger<UserFileCommandRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<int> Add(UserFileDTO userFile, CancellationToken cancellationToken)
        {
            UserFile userFile1 = new()
            {
                FileId = userFile.FileId,
                UserId = userFile.UserId,
                IsDeleted = userFile.IsDeleted
            };
            await _appDbContext.UserFiles.AddAsync(userFile1, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("files assigned to user with id {} successfully",userFile.UserId);
            return userFile1.Id;
        }
    }
}
