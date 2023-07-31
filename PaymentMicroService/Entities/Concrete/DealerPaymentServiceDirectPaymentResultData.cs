namespace PaymentMicroService.Entities.Concrete
{
    public class DealerPaymentServiceDirectPaymentResultData : IEntity
    {
        public bool IsSuccessful { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
    }
}
