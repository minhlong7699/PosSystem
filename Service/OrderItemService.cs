using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class OrderItemService : IOrderItemService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OrderItemService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OrderItemsDto> CreateOrderItemAsync(Guid orderId, OrderItemCreateUpdateDto orderItemCreate, bool trackChanges)
        {
            var orderEntity = await _repository.OrderRepository.GetOrderAsync(orderId, trackChanges);
            if (orderEntity is null) throw new OrderNotFoundException(orderId);

            var product = await _repository.ProductRepository.GetProductAsync(orderItemCreate.categoryId, orderItemCreate.ProductId, trackChanges);
            if (product is null) throw new ProductNotFoundException(orderItemCreate.ProductId);

            var orderItemsEntity = _mapper.Map<OrderItem>(orderItemCreate);
            var existingOrderItem = OrderitemsExisted(orderEntity, orderItemCreate);
            if (existingOrderItem is not null)
            {
                existingOrderItem.Quantity += orderItemCreate.Quantity;
                existingOrderItem.Price = product.ProductPrice;
                orderItemsEntity.Price = product.ProductPrice;
            }
            else
            {
                orderItemsEntity.Price = product.ProductPrice;
                if (orderItemsEntity is not null)
                {
                    _repository.OrderItemRepository.CreateOrderItemsAsync(orderId, orderItemsEntity);
                }
            }
                  
            await _repository.SaveAsync();

            var orderItemsDto = _mapper.Map<OrderItemsDto>(orderItemsEntity);

            return orderItemsDto;
        }

        public async Task<(IEnumerable<OrderItemsDto> orderItems, MetaData metaData)> GetAllOrderItemsAsync(Guid orderId, OrderItemsParameters orderItemsParameters, bool trackChanges)
        {
            var order = await _repository.OrderRepository.GetOrderAsync(orderId, trackChanges);
            if (order is null)
            {
                throw new OrderNotFoundException(orderId);
            }
            var orderItemsMetadata = await _repository.OrderItemRepository.GetAllOrderItemAsync(orderId, orderItemsParameters, trackChanges);
            var orderItemsDto = _mapper.Map<IEnumerable<OrderItemsDto>>(orderItemsMetadata);

            return (orderItems: orderItemsDto, metaData: orderItemsMetadata.MetaData);
        }

        public async Task<OrderItemsDto> GetOrderItemAsync(Guid orderId, Guid orderItemId, bool trackChanges)
        {
            var order = await _repository.OrderRepository.GetOrderAsync(orderId, trackChanges);
            if (order is null)
            {
                throw new OrderNotFoundException(orderId);
            }
            var orderItemsEntity = await _repository.OrderItemRepository.GetOrderItemAsync(orderId, orderItemId, trackChanges);
            if (orderItemsEntity is null)
            {
                throw new OrderItemNotFoundException(orderItemId);
            }
            var orderItemDto = _mapper.Map<OrderItemsDto>(orderItemsEntity);
            return orderItemDto;
        }


        public async Task UpdateOrderItemAsync(Guid orderId, OrderItemCreateUpdateDto orderItemupdate, bool trackChanges)
        {
            var orderEntity = await _repository.OrderRepository.GetOrderAsync(orderId, trackChanges);
            if (orderEntity is null) throw new OrderNotFoundException(orderId);

        }



        private OrderItem OrderitemsExisted(Order orderEntity, OrderItemCreateUpdateDto orderItemCreate)
        {
            var existingOrderItem = orderEntity.OrderItems.FirstOrDefault(item => item.ProductId.Equals(orderItemCreate.ProductId));
            var result = existingOrderItem is null ? null : existingOrderItem;
            return result;
        }

    }
}
