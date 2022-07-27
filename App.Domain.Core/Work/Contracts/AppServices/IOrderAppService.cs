using App.Domain.Core.User.DTOs;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.AppServices
{
    public interface IOrderAppService
    {
        Task<int> Add(OrderDTO order, CancellationToken cancellationToken);
        Task Update(OrderDTO order, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task DeleteOrderFile(int id, CancellationToken cancellationToken);
        Task<List<OrderDTO>> GetAll(int id, CancellationToken cancellationToken);
        Task<OrderDTO> Get(int id, CancellationToken cancellationToken);
        Task<List<OrderDTO>> GetAllExpertOrders(UserDTO expert, string query, CancellationToken cancellationToken);
        Task<List<PhysicalFileDTO>> GetAllFiles(int orderId, CancellationToken cancellationToken);

    }


}
