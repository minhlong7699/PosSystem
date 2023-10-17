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
    internal sealed class InvoiceService : IInvoiceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public InvoiceService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(InvoiceCreateUpdateDto invoiceCreate, bool trackChanges)
        {
            var order = await _repository.OrderRepository.GetOrderAsync(invoiceCreate.OrderId, trackChanges);
            if (order is null)
            {
                throw new OrderNotFoundException(invoiceCreate.OrderId);
            }
            var orderItemsOfInvoice = await _repository.OrderItemRepository.GetOrderItemsForInvoiceAsync(invoiceCreate.OrderId, trackChanges);
            // HardCode testing
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
            _repository.InvoiceRepository.CreateInvoice(invoiceEntity, trackChanges);
            var invoiceDto = _mapper.Map<InvoiceDto>(invoiceEntity);
            var orderItemsOfInvoiceDto = _mapper.Map<IEnumerable<OrderItemsDto>>(orderItemsOfInvoice);
            invoiceDto.OrderItems = orderItemsOfInvoiceDto;
            return invoiceDto;
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
    }
}
