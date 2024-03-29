﻿using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISuggestAppService _suggestAppService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderAppService> _logger;

        public OrderAppService(IOrderService orderService
            , IFileService fileService
            , IUserService userService, IServiceService serviceService
            , IHttpContextAccessor httpContextAccessor
            , ISuggestAppService suggestAppService
            , IConfiguration configuration, ILogger<OrderAppService> logger)
        {
            _orderService = orderService;
            _fileService = fileService;
            _userService = userService;
            _serviceService = serviceService;
            _httpContextAccessor = httpContextAccessor;
            _suggestAppService = suggestAppService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task AcceptOrderSuggest(int suggestId, CancellationToken cancellationToken)
        {
            var suggest = await _suggestAppService.Get(suggestId, cancellationToken);
            suggest.IsConfirmedByCustomer=true;
            await _suggestAppService.Update(suggest, cancellationToken);
            var order=await _orderService.Get(suggest.OrderId, cancellationToken);
            order.FinalPrice = suggest.SuggestedPrice;
            order.StatusId++;
            order.ConfirmedExpertId = suggest.ExpertId;
            order.IsConfirmedByCustomer = true;
            await _orderService.Update(order, cancellationToken);
            _logger.LogInformation("suggest with id {id} accepted for order with id {id}", suggest.Id, suggest.OrderId);

        }

        public async Task<int> AddNewOrder(OrderDTO order, List<IFormFile> files, CancellationToken cancellationToken)
        {
            var currentUser = await _userService.GetCurrentUserFullInfo();
            order.CustomerId = currentUser.Id;
            order.Id = 0;
            var service = await _serviceService.Get(order.ServiceId, cancellationToken);
            order.FinalPrice = service.Price;
            var result = await _orderService.Add(order, cancellationToken);
            var fileIds = await _fileService.UploadFileAsync(files, cancellationToken);
            await _orderService.AddOrderFiles(result, fileIds, cancellationToken);
            if (result != 0)
            {
                _logger.LogInformation("new order {action} successfully", "add");
            }
            else
            {
                _logger.LogWarning("{action} new order failed", "add");
            }
            return result;
        }

        public async Task ChangeOrderStatus(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderService.Get(orderId, cancellationToken);
            order.StatusId++;
            await _orderService.Update(order, cancellationToken);
            _logger.LogInformation("status of order with id {id} changed to level {statusId}",orderId, order.StatusId);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderService.Delete(id, cancellationToken);
        }

        public async Task DeleteOrderFile(int id, CancellationToken cancellationToken)
        {
            var result = await _fileService.Get(id, cancellationToken);
            var physicalFilePath = result.Path;
            await _fileService.DeletePhysicalFile(physicalFilePath, cancellationToken);
            await _orderService.DeleteOrderFile(id, cancellationToken);
            _logger.LogInformation("files for order with id {id} {action} successfully", id, "Delete");
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
