namespace OnlineShopWebApp.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public UserDeliveryInfoViewModel User { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public OrderStatusViewModel Status { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public OrderViewModel()
        {
            Id = Guid.NewGuid();
            Status = OrderStatusViewModel.Created;
            CreatedDateTime = DateTime.Now;
        }
        public decimal Cost
        {
            get
            {
                return Items?.Sum(x => x.Cost) ?? 0;
            }
        }
    }
}
