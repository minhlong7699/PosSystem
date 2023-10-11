using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IServiceManager
    {
        ICategoryService CategoryService { get; }
        IInvoiceService InvoiceService { get; }
        IOrderItemService OrderItemService { get; }
        IOrderService OrderService { get; }
        IPaymentService PaymentService { get; }
        IProductService ProductService { get; }
        IPromotionService PromotionService { get; }
        ISupplierService SupplierService { get; }
        ITableService TablesService { get; }
        ITaxService TaxService { get; }
        IUserAuthenticationService UserAuthenticationService { get; }
        IUserService UserService { get; }
        IUserRoleService UserRoleService { get; }
        IAuthenticationService AuthenticationService { get; }

    }
}
