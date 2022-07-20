using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;

        public OrderAppService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<int> Add(OrderDTO order, CancellationToken cancellationToken)
        {
            var result = await _orderService.Add(order, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderService.Delete(id, cancellationToken);
        }

        public async Task<OrderDTO> Get(int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.Get(id, cancellationToken);
            return order;
        }

        public async Task<List<OrderDTO>> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAll(cancellationToken);
            return orders;
        }

        public async Task Update(OrderDTO order, CancellationToken cancellationToken)
        {
            await _orderService.Update(order, cancellationToken);
        }
    }
}
