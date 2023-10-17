using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IRepositoryManager
    {
        ICategoryRepository CategoryRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IProductRepository ProductRepository { get; }
        IPromotionRepository PromotionRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ITableRepository TablesRepository { get; }
        ITaxRepository TaxRepository { get; }
        Task SaveAsync();
    }
}
