namespace PaymentMicroService.Entities.Concrete
{
    public class DealerPaymentServicePaymentResult : IEntity
    {
        public string Data { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string Exception { get; set; }
    }
}
