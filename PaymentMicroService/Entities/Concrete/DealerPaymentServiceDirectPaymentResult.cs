namespace PaymentMicroService.Entities.Concrete
{
    public class DealerPaymentServiceDirectPaymentResult : IEntity
    {
        public DealerPaymentServiceDirectPaymentResultData Data { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string Exception { get; set; }
    }
}
