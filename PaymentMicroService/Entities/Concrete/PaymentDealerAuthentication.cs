namespace PaymentMicroService.Entities.Concrete
{
    public class PaymentDealerAuthentication : IEntity
    {
        public string DealerCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CheckKey { get; set; }
    }
}
