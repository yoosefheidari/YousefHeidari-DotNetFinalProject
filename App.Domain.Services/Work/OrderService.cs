using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Work
{
    public class OrderService : IOrderService
    {
        private readonly IOrderCommandRepository _orderCommandRepository;
        private readonly IOrderQueryRepository _orderQueryRepository;

        public OrderService(IOrderQueryRepository orderQueryRepository, IOrderCommandRepository orderCommandRepository)
        {
            _orderQueryRepository = orderQueryRepository;
            _orderCommandRepository = orderCommandRepository;
        }

        public async Task<int> Add(OrderDTO order, CancellationToken cancellationToken)
        {
            order.CreationDate = DateTimeOffset.Now;
            order.IsDeleted = false;
            order.StatusId = 1;

            var result = await _orderCommandRepository.Add(order, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderCommandRepository.Delete(id, cancellationToken);
        }

        public async Task<OrderDTO> Get(int id, CancellationToken cancellationToken)
        {
            var order = await _orderQueryRepository.Get(id, cancellationToken);
            return order;
        }

        public async Task<List<OrderDTO>> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _orderQueryRepository.GetAll(cancellationToken);
            return orders;
        }

        public async Task Update(OrderDTO order, CancellationToken cancellationToken)
        {
            await _orderCommandRepository.Update(order, cancellationToken);
        }
    }
}
