using AutoMapper;
using Entity.Models;
using Shared.DataTransferObjects;
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
            // UserRole -> UserRoleDto
            CreateMap<UserRole, UserRoleDto>();
            // Tax -> TaxDto
            CreateMap<Tax, TaxDto>();
            // Order -> OrderDto
            CreateMap<Order, OrderDto>();
            // Tax -> TaxDto
            CreateMap<Tax, TaxDto>();
            // Payemnt -> paymentDto
            CreateMap<Payment, PaymentDto>();
            // Table -> TableDto
            CreateMap<Table, TableDto>();
            // User -> UserDto
            CreateMap<User, UserDto>();
            // OrderItems -> OrderItemsDto();
            CreateMap<OrderItem, OrderItemsDto>();
            // CategoryUpdateCreateDto -> Category
            CreateMap<CategoryUpdateCreateDto, Category>();
            // ProductUpdateCreateDto -> Product
            CreateMap<ProductUpdateCreateDto, Product>().ReverseMap();
            // PaymentUpdateCreateDto -> Payment
            CreateMap<PaymentUpdateCreateDto, Payment>().ReverseMap();
            // UserUpdateCreateDto -> User
            CreateMap<UserUpdateCreateDto, User>();
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
            
        }
    }
}
