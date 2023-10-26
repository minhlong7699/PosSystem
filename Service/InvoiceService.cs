using AutoMapper;
using Contract;
using Contract.Service;
using Contract.Service.UserProvider;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects.Invoice;
using Shared.DataTransferObjects.OrderItem;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class InvoiceService : IInvoiceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public InvoiceService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper, IUserProvider userProvider)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(InvoiceCreateUpdateDto invoiceCreate, bool trackChanges)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var orderEntity = await _repository.OrderRepository.GetOrderAsync(invoiceCreate.OrderId, trackChanges);
            if (orderEntity is null)
            {
                throw new OrderNotFoundException(invoiceCreate.OrderId);
            }
            orderEntity.IsActive = false;
            var tableEntity = await _repository.TablesRepository.GetTableAsync(orderEntity.TableId, trackChanges);
            tableEntity.IsOccupied = false;
            var orderItemsOfInvoice = await _repository.OrderItemRepository.GetOrderItemsForInvoiceAsync(invoiceCreate.OrderId, trackChanges);
            var invoiceEntity = _mapper.Map<Invoice>(invoiceCreate);
            decimal totalAmount = 0;
            foreach (var item in orderItemsOfInvoice)
            {
                var product = await _repository.ProductRepository.GetProductForInvoiceAsync(item.ProductId, trackChanges);
                if (product is not null)
                {
                    decimal itemAmount = (decimal)(product.ProductPrice * item.Quantity);
                    totalAmount += itemAmount;
                }
            }
            invoiceEntity.TotalAmount = totalAmount;
            invoiceEntity.InvoiceDate = DateTime.Now;
            invoiceEntity.Status = "Success";
            invoiceEntity.CreateAuditFields(userId);
            _repository.InvoiceRepository.CreateInvoice(invoiceEntity, trackChanges);
            await _repository.SaveAsync();
            var invoiceDto = _mapper.Map<InvoiceDto>(invoiceEntity);
            var orderItemsOfInvoiceDto = _mapper.Map<IEnumerable<OrderItemsDto>>(orderItemsOfInvoice);
            invoiceDto.OrderItems = orderItemsOfInvoiceDto;
            return invoiceDto;
        }

        public async Task DelteInvoiceAsync(Guid invoiceId, bool trackChanges)
        {
            var invoice = await  _repository.InvoiceRepository.GetInvoiceAsync(invoiceId, trackChanges);
            if (invoice is null) throw new InvoiceNotFoundException(invoiceId);
            _repository.InvoiceRepository.DeleteInvoice(invoice);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<InvoiceDto> invoices, MetaData metadata)> GetAllInvoiceAsync(InvoiceParameter invoiceParameter, bool trackChanges)
        {
            var invoiceMetadata = await _repository.InvoiceRepository.GetAllInvoicesAsync(invoiceParameter, trackChanges);
            var invoiceDto = _mapper.Map<IEnumerable<InvoiceDto>>(invoiceMetadata);
            return (invoices: invoiceDto, metadata: invoiceMetadata.MetaData);
        }

        public async Task<InvoiceDto> GetInvoiceAsync(Guid invoiceId, bool trackChanges)
        {
            var invoice = await _repository.InvoiceRepository.GetInvoiceAsync(invoiceId, trackChanges);
            if (invoice is null) throw new InvoiceNotFoundException(invoiceId);
            var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
            return invoiceDto;

        }

        public async Task UpdateInvoiceAsync(Guid invoiceId, InvoiceUpdateDto invoiceUpdate, bool trackChanges)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var invoiceEntity = await _repository.InvoiceRepository.GetInvoiceAsync(invoiceId, trackChanges);
            invoiceEntity.UpdateAuditFields(userId);
            _mapper.Map(invoiceUpdate, invoiceEntity);
            await _repository.SaveAsync();          
        }
    }
}
