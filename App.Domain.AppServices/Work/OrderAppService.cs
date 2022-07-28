using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
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
        private readonly IFileService _fileService;
        private readonly IUploadService _uploadService;
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;

        public OrderAppService(IOrderService orderService, IFileService fileService, IUploadService uploadService, IUserService userService, IServiceService serviceService)
        {
            _orderService = orderService;
            _fileService = fileService;
            _uploadService = uploadService;
            _userService = userService;
            _serviceService = serviceService;
        }

        public async Task<int> AddNewOrder(OrderDTO order, List<IFormFile> files, CancellationToken cancellationToken)
        {
            var currentUser = await _userService.GetCurrentUser();
            order.CustomerId = currentUser.Id;
            var service = await _serviceService.Get(order.ServiceId, cancellationToken);
            order.FinalPrice = service.Price;
            var result = await _orderService.Add(order, cancellationToken);
            var fileIds = await _uploadService.UploadFileAsync(files, cancellationToken);
            await _orderService.AddOrderFiles(result, fileIds, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderService.Delete(id, cancellationToken);
        }

        public async Task DeleteOrderFile(int id, CancellationToken cancellationToken)
        {
            var result = await _fileService.Get(id, cancellationToken);
            var physicalFilePath = result.Path;
            File.Delete(physicalFilePath);
            await _orderService.DeleteOrderFile(id, cancellationToken);
        }

        public async Task<OrderDTO> Get(int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.Get(id, cancellationToken);
            return order;
        }

        public async Task<List<OrderDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAll(id, cancellationToken);
            return orders;
        }

        public Task<List<OrderDTO>> GetAllExpertOrders(UserDTO expert, string query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhysicalFileDTO>> GetAllFiles(int orderId, CancellationToken cancellationToken)
        {
            var paths = await _orderService.GetAllFiles(orderId, cancellationToken);
            return paths;
        }

        public async Task Update(OrderDTO order, CancellationToken cancellationToken)
        {
            await _orderService.Update(order, cancellationToken);
        }
    }
}
