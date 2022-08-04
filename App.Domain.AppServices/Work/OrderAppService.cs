using App.Domain.Core.User.Contracts.Services;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISuggestAppService _suggestAppService;

        public OrderAppService(IOrderService orderService, IFileService fileService, IUploadService uploadService, IUserService userService, IServiceService serviceService, IHttpContextAccessor httpContextAccessor, ISuggestAppService suggestAppService)
        {
            _orderService = orderService;
            _fileService = fileService;
            _uploadService = uploadService;
            _userService = userService;
            _serviceService = serviceService;
            _httpContextAccessor = httpContextAccessor;
            _suggestAppService = suggestAppService;
        }

        public async Task AcceptOrderSuggest(int suggestId, CancellationToken cancellationToken)
        {
            var suggest = await _suggestAppService.Get(suggestId, cancellationToken);
            var order=await _orderService.Get(suggest.OrderId, cancellationToken);
            order.FinalPrice = suggest.SuggestedPrice;
            order.StatusId++;
            order.ConfirmedExpertId = suggest.ExpertId;
            order.IsConfirmedByCustomer = true;
            await _orderService.Update(order, cancellationToken);
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

        public async Task ChangeOrderStatus(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderService.Get(orderId, cancellationToken);
            order.StatusId++;
            await _orderService.Update(order, cancellationToken);
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
            var files=await _orderService.GetAllFiles(id, cancellationToken);
            order.Photos = files;
            return order;
        }

        public async Task<List<OrderDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAll(id, cancellationToken);
            return orders;
        }

        public async Task<List<OrderDTO>> GetAllExpertOrders(string query, CancellationToken cancellationToken)
        {
            var currentUserUsername = _httpContextAccessor.HttpContext.User.Identity.Name;
            var currentUser = await _userService.GetUserByUserName(currentUserUsername);
            var orders = await _orderService.GetAllExpertOrders(currentUser, query, cancellationToken);
            return orders;
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
