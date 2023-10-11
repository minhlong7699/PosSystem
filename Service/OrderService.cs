using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects;
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

        public OrderService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateUpdateDto orderCreate, bool trackChanges)
        {

            var user = await _repository.UserRepository.GetUserAsync(orderCreate.UserId, trackChanges);
            if (user is null) throw new UserNotFoundException(orderCreate.UserId);
            var payment = await _repository.PaymentRepository.GetPaymentAsync(orderCreate.PaymentId, trackChanges);
            if (payment is null) throw new PaymentNotFoundException(orderCreate.PaymentId);
            var tax = await _repository.TaxRepository.GetTaxAsync(orderCreate.TaxId, trackChanges);
            if(tax is null) throw new TaxNotFoundExeception(orderCreate.TaxId);
            var promotion = await _repository.PromotionRepository.GetPromotionAsync(orderCreate.PromotionId, trackChanges);
            if (promotion is null) throw new PromotionNotFoundException(orderCreate.PromotionId);
            

            var orderEntity = _mapper.Map<Order>(orderCreate);

            orderEntity.CreatedAt = DateTime.Now;
            orderEntity.CreatedBy = "Admin";
            orderEntity.UpdatedAt = DateTime.Now;
            orderEntity.UpdatedBy = "Admin";
            _repository.OrderRepository.CreateOrder(orderEntity);
            await _repository.SaveAsync();
            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            return orderDto;
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
    }
}
