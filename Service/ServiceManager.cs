using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;

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
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, ILogger logger, IMapper mapper, IUploadImageService uploadImageService, IConfiguration configuration, UserManager<User> userManager)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, logger, mapper));
            _invoiceService = new Lazy<IInvoiceService>(() => new InvoiceService(repositoryManager, logger, mapper));
            _orderItemService = new Lazy<IOrderItemService>(() => new OrderItemService(repositoryManager, logger, mapper));
            _orderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, logger, mapper, userManager));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(repositoryManager, logger, mapper));
            _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, logger, mapper, uploadImageService));
            _promotionService = new Lazy<IPromotionService>(() => new PromotionService(repositoryManager, logger, mapper));
            _supplierService = new Lazy<ISupplierService>(() => new SupplierService(repositoryManager, logger, mapper));
            _tableService = new Lazy<ITableService>(() => new TableService(repositoryManager, logger, mapper));
            _taxService = new Lazy<ITaxService>(() => new TaxService(repositoryManager, logger, mapper));
        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public ICategoryService CategoryService => _categoryService.Value;

        public IInvoiceService InvoiceService => _invoiceService.Value;

        public IOrderItemService OrderItemService => _orderItemService.Value;

        public IOrderService OrderService => _orderService.Value;

        public IPaymentService PaymentService => _paymentService.Value;

        public IProductService ProductService => _productService.Value;

        public IPromotionService PromotionService => _promotionService.Value;

        public ISupplierService SupplierService => _supplierService.Value;

        public ITableService TablesService => _tableService.Value;

        public ITaxService TaxService => _taxService.Value;
    }
}
