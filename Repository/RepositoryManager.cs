using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly RepositoryContext _repositoryContext;


        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IInvoiceRepository> _invoiceRepository;
        private readonly Lazy<IOrderItemRepository> _orderItemRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IPaymentRepository> _paymentRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IPromotionRepository> _promotionRepository;
        private readonly Lazy<ISupplierRepository> _supplierRepository;
        private readonly Lazy<ITableRepository> _tableRepository;
        private readonly Lazy<ITaxRepository> _taxRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
            _invoiceRepository = new Lazy<IInvoiceRepository>(() => new InvoiceRepository(repositoryContext));
            _orderItemRepository = new Lazy<IOrderItemRepository>(() => new OrderItemRepository(repositoryContext));
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(repositoryContext));
            _paymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(repositoryContext));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryContext));
            _promotionRepository = new Lazy<IPromotionRepository>(() => new PromotionRepository(repositoryContext));
            _supplierRepository = new Lazy<ISupplierRepository>(() => new SupplierRepository(repositoryContext));
            _tableRepository = new Lazy<ITableRepository>(() => new TableRepository(repositoryContext));
            _taxRepository = new Lazy<ITaxRepository>(() => new TaxRepository(repositoryContext));
        }
        public ICategoryRepository CategoryRepository => _categoryRepository.Value;

        public IInvoiceRepository InvoiceRepository => _invoiceRepository.Value;

        public IOrderItemRepository OrderItemRepository => _orderItemRepository.Value;

        public IOrderRepository OrderRepository => _orderRepository.Value;

        public IPaymentRepository PaymentRepository => _paymentRepository.Value;

        public IProductRepository ProductRepository => _productRepository.Value;

        public IPromotionRepository PromotionRepository => _promotionRepository.Value;

        public ISupplierRepository SupplierRepository => _supplierRepository.Value;

        public ITableRepository TablesRepository => _tableRepository.Value;

        public ITaxRepository TaxRepository => _taxRepository.Value;



        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _repositoryContext.Dispose();
        }
    }
}
