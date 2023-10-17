using AutoMapper;
using Entity.Models;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Authentication;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PosSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category -> CategoryDto
            CreateMap<Category, CategoryDto>();
            // Product -> ProductDto
            CreateMap<Product, ProductDto>();
            // Promotion -> PromotionDto
            CreateMap<Promotion, PromotionDto>();
            // Tax -> TaxDto
            CreateMap<Tax, TaxDto>();
            // Order -> OrderDto
            CreateMap<Order, OrderDto>();
            // Tax -> TaxDto
            CreateMap<Tax, TaxDto>();
            // Invoice -> InvoiceDto
            CreateMap<Invoice, InvoiceDto>();
            // Payemnt -> paymentDto
            CreateMap<Payment, PaymentDto>();
            // Table -> TableDto
            CreateMap<Table, TableDto>();
            // OrderItems -> OrderItemsDto();
            CreateMap<OrderItem, OrderItemsDto>();
            // InvoiceUpdateCreateDto -> Invoice
            CreateMap<InvoiceCreateUpdateDto, Invoice>();
            // CategoryUpdateCreateDto -> Category
            CreateMap<CategoryUpdateCreateDto, Category>();
            // ProductUpdateCreateDto -> Product
            CreateMap<ProductUpdateCreateDto, Product>().ReverseMap();
            // PaymentUpdateCreateDto -> Payment
            CreateMap<PaymentUpdateCreateDto, Payment>().ReverseMap();
            // OrderUpdateCreateDto -> Order
            CreateMap<OrderCreateUpdateDto, Order>();
            // TableUpdateCreateDto -> Table
            CreateMap<TableUpdateCreateDto, Table>();
            // TaxCrateUpdateDto -> Tax
            CreateMap<TaxCreateUpdateDto, Tax>();
            // OrderItemUpdateCreateDto -> Order
            CreateMap<OrderItemCreateUpdateDto, OrderItem>();
            // orderItemUpdate -> OrderItems
            CreateMap<OrderItemUpdateDto, OrderItem>();
            // Supplier -> SupplierDto
            CreateMap<Supplier, SupllierDto>();
            // UserForRegistrationDto -> User
            CreateMap<UserForRegistrationDto, User>();


        }
    }
}
