namespace Orders.Models
{
     /// <summary>
      /// 受注明細
      /// </summary>
    public class OrderDetail
    {
        public OrderDetail(ulong orderId, ulong orderDetailId, string itemCode)
        {
            OrderId = orderId;
            OrderDetailId = orderDetailId;
            ItemCode = itemCode ?? throw new ArgumentNullException(nameof(itemCode));
        }

        /// <summary>ID</summary>
        public ulong OrderId { get; set; }
        public ulong OrderDetailId { get; set; }
        /// <summary>品目コード</summary>
        public string ItemCode { get; set; }
        /// <summary>数量</summary>
        public decimal Quantity { get; set; }
        /// <summary>個数</summary>
        public int Count { get; set; }
        /// <summary>回答納期</summary>
        public DateTime? AnswerLimitDate { get; set; }
        /// <summary>希望納期</summary>
        public DateTime DesiredLimitDate { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is OrderDetail detail &&
                     OrderId == detail.OrderId &&
                     OrderDetailId == detail.OrderDetailId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, OrderDetailId);
        }
    }
}
