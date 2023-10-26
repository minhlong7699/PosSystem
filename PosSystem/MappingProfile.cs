using AutoMapper;
using Entity.Models;
using Shared.DataTransferObjects.Authentication;
using Shared.DataTransferObjects.Category;
using Shared.DataTransferObjects.Invoice;
using Shared.DataTransferObjects.Order;
using Shared.DataTransferObjects.OrderItem;
using Shared.DataTransferObjects.Payment;
using Shared.DataTransferObjects.Product;
using Shared.DataTransferObjects.Promotion;
using Shared.DataTransferObjects.Supplier;
using Shared.DataTransferObjects.Table;
using Shared.DataTransferObjects.Tax;
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
            // InvoiceUpdateDto -> Invoice
            CreateMap<InvoiceUpdateDto, Invoice>();
            // PromotionCreateUpdateDto -> Promotion
            CreateMap<PromotionUpdateCreateDto, Promotion>();


        }
    }
}
