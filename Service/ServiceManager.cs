using Contract;
using Contract.Service;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IInvoiceService> _invoiceService;
        private readonly Lazy<IOrderItemService> _orderItemService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IPromotionService> _promotionService;
        private readonly Lazy<ISupplierService> _supplierService;
        private readonly Lazy<ITableService> _tableService;
        private readonly Lazy<ITaxService> _taxService;
        private readonly Lazy<IUserAuthenticationService> _userAuthenticationService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IUserRoleService> _userRoleService;

        public ServiceManager(IRepositoryManager repositoryManager, ILogger logger)
        {
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, logger));
            _invoiceService = new Lazy<IInvoiceService>(() => new InvoiceService(repositoryManager, logger));
            _orderItemService = new Lazy<IOrderItemService>(() => new OrderItemService(repositoryManager, logger));
            _orderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, logger));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(repositoryManager, logger));
            _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, logger));
            _promotionService = new Lazy<IPromotionService>(() => new PromotionService(repositoryManager, logger));
            _supplierService = new Lazy<ISupplierService>(() => new SupplierService(repositoryManager, logger));
            _tableService = new Lazy<ITableService>(() => new TableService(repositoryManager, logger));
            _taxService = new Lazy<ITaxService>(() => new TaxService(repositoryManager, logger));
            _userAuthenticationService = new Lazy<IUserAuthenticationService>(() => new UserAuthenticationService(repositoryManager, logger));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, logger));
            _userRoleService = new Lazy<IUserRoleService>(() => new UserRoleService(repositoryManager, logger));
        }
        public ICategoryService CategoryService => _categoryService.Value;

        public IInvoiceService InvoiceService => _invoiceService.Value;

        public IOrderItemService OrderItemService => _orderItemService.Value;

        public IOrderService OrderService => _orderService.Value;

        public IPaymentService paymentService => _paymentService.Value;

        public IProductService ProductService => _productService.Value;

        public IPromotionService PromotionService => _promotionService.Value;

        public ISupplierService SupplierService => _supplierService.Value;

        public ITableService TablesService => _tableService.Value;

        public ITaxService TaxService => _taxService.Value;

        public IUserAuthenticationService UserAuthenticationService => _userAuthenticationService.Value;

        public IUserService UserService => _userService.Value;

        public IUserRoleService userRoleService => _userRoleService.Value;
    }
}
