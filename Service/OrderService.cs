using AutoMapper;
using Contract;
using Contract.Service;
using Contract.Service.UserProvider;
using Entity.Exceptions;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Shared.DataTransferObjects.Order;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserProvider _userProvider;

        public OrderService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper, UserManager<User> userManager, IUserProvider userProvider)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _userProvider = userProvider;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateUpdateDto orderCreate, bool trackChanges)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var payment = await _repository.PaymentRepository.GetPaymentAsync(orderCreate.PaymentId, trackChanges);
            if (payment is null) throw new PaymentNotFoundException(orderCreate.PaymentId);
            var tax = await _repository.TaxRepository.GetTaxAsync(orderCreate.TaxId, trackChanges);
            if(tax is null) throw new TaxNotFoundExeception(orderCreate.TaxId);
            var promotion = await _repository.PromotionRepository.GetPromotionAsync(orderCreate.PromotionId, trackChanges);
            if (promotion is null) throw new PromotionNotFoundException(orderCreate.PromotionId);
            var table = await _repository.TablesRepository.GetTableAsync(orderCreate.TableId, trackChanges);
            if (table is null) throw new TableNotFoundException(orderCreate.TableId);
            table.IsOccupied = true;
            var orderEntity = _mapper.Map<Order>(orderCreate);
            orderEntity.CreateAuditFields(userId);
            _repository.OrderRepository.CreateOrder(orderEntity);
            await _repository.SaveAsync();
            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            return orderDto;
        }

        public async Task DeleteOrderAsync(Guid orderId, bool trackChanges)
        {
            var order = await _repository.OrderRepository.GetOrderAsync(orderId, trackChanges);
            if (order is null) throw new OrderNotFoundException(orderId);
            _repository.OrderRepository.DeleteOrder(order);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<OrderDto> orders, MetaData metaData)> GetAllOrderAsync(OrderParameters orderParameters, bool trackChanges)
        {
            var orderMetadata = await _repository.OrderRepository.GetAllOrderAsync(orderParameters, trackChanges);
            var orderDto = _mapper.Map<IEnumerable<OrderDto>>(orderMetadata);
            return (orderDto, orderMetadata.MetaData);
            
        }

        public async Task<OrderDto> GetOrderAsync(Guid orderId, bool trackChanges)
        {
            var order = await _repository.OrderRepository.GetOrderAsync(orderId, trackChanges);
            if(order is null)
            {
                throw new OrderNotFoundException(orderId);
            }
            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task UpdateOrderAsync(Guid orderId, OrderCreateUpdateDto orderUpdate, bool trackChanges)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var orderEntity = await _repository.OrderRepository.GetOrderAsync(orderId, trackChanges);
            if (orderEntity is null) throw new OrderNotFoundException(orderId);
            orderEntity.UpdateAuditFields(userId);
            _mapper.Map(orderUpdate, orderEntity);
            await _repository.SaveAsync();
        }
    }
}
