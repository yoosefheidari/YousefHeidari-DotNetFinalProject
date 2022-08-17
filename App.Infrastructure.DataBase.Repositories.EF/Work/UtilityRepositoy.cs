using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class UtilityRepositoy : IUtilityRepositoy
    {
        private readonly IServiceQueryRepository _serviceQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IOrderQueryRepository _orderQueryRepository;

        public UtilityRepositoy(IServiceQueryRepository serviceQueryRepository, IUserQueryRepository userQueryRepository, IOrderQueryRepository orderQueryRepository)
        {
            _serviceQueryRepository = serviceQueryRepository;
            _userQueryRepository = userQueryRepository;
            _orderQueryRepository = orderQueryRepository;
        }

        public async Task<StatisticsDTO> GetLatestStatistics(CancellationToken cancellationToken)
        {
            var services = await _serviceQueryRepository.GetAll(0, cancellationToken);
            var orders = await _orderQueryRepository.GetAll(0, cancellationToken);
            var users = await _userQueryRepository.GetAll(0, null, cancellationToken);
            var totalSell = orders.Sum(x => x.FinalPrice);
            var statistics = new StatisticsDTO()
            {
                TotalOrders = orders.Count,
                TotalServices = services.Count,
                TotalUsers = users.Count,
            };
            return statistics;
        }
    }
}
