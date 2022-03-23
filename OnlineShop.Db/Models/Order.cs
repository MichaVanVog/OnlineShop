namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public UserDeliveryInfo User { get; set; }
        public List<CartItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public Order()
        {
            Status = OrderStatus.Created;
            CreatedDateTime = DateTime.Now;
        }
    }
}
