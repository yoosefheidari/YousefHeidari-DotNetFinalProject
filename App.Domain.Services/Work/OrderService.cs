using App.Domain.Core.User.DTOs;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IOrderFileCommandRepository _orderFileCommandRepository;

        public OrderService(IOrderQueryRepository orderQueryRepository, IOrderCommandRepository orderCommandRepository, IConfiguration configuration, IOrderFileCommandRepository orderFileCommandRepository)
        {
            _orderQueryRepository = orderQueryRepository;
            _orderCommandRepository = orderCommandRepository;
            _configuration = configuration;
            _orderFileCommandRepository = orderFileCommandRepository;
        }

        public async Task<int> Add(OrderDTO order, CancellationToken cancellationToken)
        {
            order.CreationDate = DateTimeOffset.Now;
            order.IsDeleted = false;
            order.StatusId = 1;
            order.ConfirmedExpertId = null;
            order.IsConfirmedByCustomer = false;
            order.Description =order.Description;

            var result = await _orderCommandRepository.Add(order, cancellationToken);
            return result;
        }

        public async Task<bool> AddOrderFiles(int orderId, List<int> fileIds, CancellationToken cancellationToken)
        {
            foreach (var fileId in fileIds)
            {
                OrderFileDTO orderFile = new()
                {
                    FileId = fileId,
                    OrderId = orderId,
                    IsDeleted = false,
                };
                var result = await _orderFileCommandRepository.Add(orderFile, cancellationToken);
            }
            return true;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderCommandRepository.Delete(id, cancellationToken);
        }

        public async Task DeleteOrderFile(int id, CancellationToken cancellationToken)
        {
            await _orderCommandRepository.DeleteOrderFile(id, cancellationToken);
        }

        public async Task<OrderDTO> Get(int id, CancellationToken cancellationToken)
        {
            var order = await _orderQueryRepository.Get(id, cancellationToken);
            return order;
        }

        public async Task<List<OrderDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            var orders = await _orderQueryRepository.GetAll(id, cancellationToken);
            return orders;
        }

        public Task<List<OrderDTO>> GetAllExpertOrders(UserDTO expert, string query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhysicalFileDTO>> GetAllFiles(int orderId, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetSection("DownloadPath").Value;
            var paths = await _orderQueryRepository.GetAllFiles(orderId, cancellationToken);
            foreach (var path in paths)
            {
                path.Path = rootPath + "/" + path.Path;
            }
            return paths;
        }

        public async Task Update(OrderDTO order, CancellationToken cancellationToken)
        {
            await _orderCommandRepository.Update(order, cancellationToken);
        }
    }
}
