﻿using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class SuggestAppService : ISuggestAppService
    {
        private readonly ISuggestService _suggestService;
        private readonly ILogger<SuggestAppService> _logger;

        public SuggestAppService(ISuggestService suggestService, ILogger<SuggestAppService> logger)
        {
            _suggestService = suggestService;
            _logger = logger;
        }

        public async Task<int> CreateSuggest(int orderId, int expertId, int price, string description, CancellationToken cancellationToken)
        {
            SuggestDTO suggest = new()
            {
                SuggestedPrice = price,
                CreationDate = DateTimeOffset.Now,
                Description = description,
                ExpertId = expertId,
                IsConfirmedByCustomer = false,
                IsDeleted = false,
                OrderId = orderId,
            };
            var result = await _suggestService.Add(suggest, cancellationToken);
            if (result != 0)
            {
                _logger.LogInformation("new suggest {action} successfully", "add");
            }
            else
            {
                _logger.LogWarning("{action} new suggest failed", "add");
            }
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _suggestService.Delete(id, cancellationToken);
            _logger.LogInformation("suggest with id {id} {action} successfully", id, "Delete");
        }

        public async Task EditSuggest(int suggestId, int price, string description, CancellationToken cancellationToken)
        {
            var suggest = await _suggestService.Get(suggestId, cancellationToken);
            suggest.Description = description;
            suggest.SuggestedPrice = price;
            await _suggestService.Update(suggest, cancellationToken);

        }

        public async Task<SuggestDTO> Get(int id, CancellationToken cancellationToken)
        {
            var suggest = await _suggestService.Get(id, cancellationToken);
            return suggest;
        }

        public async Task<List<SuggestDTO>> GetAll(CancellationToken cancellationToken)
        {
            var suggests = await _suggestService.GetAll(cancellationToken);
            return suggests;
        }

        public async Task<List<SuggestDTO>> GetAll(int OrderId, CancellationToken cancellationToken)
        {
            var suggests = await _suggestService.GetAll(OrderId, cancellationToken);
            return suggests;
        }

        public async Task Update(SuggestDTO suggest, CancellationToken cancellationToken)
        {
            await _suggestService.Update(suggest, cancellationToken);
        }
    }
}
