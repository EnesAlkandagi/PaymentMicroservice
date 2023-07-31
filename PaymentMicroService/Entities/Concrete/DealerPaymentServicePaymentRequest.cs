namespace PaymentMicroService.Entities.Concrete
{
    public class DealerPaymentServicePaymentRequest: IEntity
    {
        public PaymentDealerAuthentication PaymentDealerAuthentication { get; set; }
        public PaymentDealerRequest PaymentDealerRequest { get; set; }
    }
}
