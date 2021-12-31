using BasicBuildingBlocks.Domain;

namespace Orders.Models
{
    /// <summary>
    /// 受注
    /// </summary>
    public class Order: IAggregateRoot
    {
        public Order(ulong orderId, string orderNumber)
        {
            OrderId = orderId;
            OrderNumber = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
        }

        /// <summary>ID</summary>
        public ulong OrderId { get; set; }
        /// <summary>受注番号</summary>
        public string OrderNumber { get; set; }
        /// <summary>受注日付</summary>
        public DateTime OrderDate { get; set; }

        /// <summary>数量</summary>
        public decimal Quantity { get { return Details?.Sum(d => d.Quantity) ?? 0M; } }
        /// <summary>個数</summary>
        public int Count { get { return Details?.Sum(d => d.Count) ?? 0; } }
        /// <summary>売上先コード</summary>
        public ulong SalesDestinationCode { get; set; }
        /// <summary>売上先部門コード</summary>
        public ulong SalesDepartmentCode { get; set; }
        /// <summary>送付先コード</summary>
        public ulong ShippingDestinationCode { get; set; }
        /// <summary>送付先部門コード</summary>
        public ulong ShippingDepartmentCode { get; set; }
       
        public IEnumerable<OrderDetail>? Details { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Order order &&
                     OrderId == order.OrderId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId);
        }
    }

   
}
