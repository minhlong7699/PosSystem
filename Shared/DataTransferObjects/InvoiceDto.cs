namespace Shared.DataTransferObjects
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Status { get; set; }
        public Guid OrderId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<OrderItemsDto>? OrderItems { get; set; }
    }
}
