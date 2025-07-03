namespace BlazorDemo.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public Address DeliveryAddress { get; set; } = new Address();
    }
}
