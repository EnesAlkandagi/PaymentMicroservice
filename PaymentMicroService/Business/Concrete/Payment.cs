using Nancy.Json;
using PaymentMicroService.Business.Abstract;
using PaymentMicroService.Entities.Concrete;
using PaymentMicroService.Helpers;
using System.Net;

namespace PaymentMicroService.Business.Concrete
{
    public class Payment 
    {
        public string GetPayment()
        {
            DealerPaymentServicePaymentRequest request = new DealerPaymentServicePaymentRequest();
            request.PaymentDealerAuthentication = new PaymentDealerAuthentication();
            request.PaymentDealerAuthentication.DealerCode = "250";
            request.PaymentDealerAuthentication.Username = "testuser";
            request.PaymentDealerAuthentication.Password = "KLRSMGLSX";
            ConvertSHA256Hash convertSHA256Hash = new ConvertSHA256Hash();
            request.PaymentDealerAuthentication.CheckKey = convertSHA256Hash.SHA256Hash("250" + "MK" + "testuser" + "PD" + "KLRSMGLSX");

            request.PaymentDealerRequest = new PaymentDealerRequest();
            request.PaymentDealerRequest.CardHolderFullName = "Ahmet Yılmaz";
            request.PaymentDealerRequest.CardNumber = "5555666677778888";
            request.PaymentDealerRequest.ExpMonth = "2017";
            request.PaymentDealerRequest.ExpYear = "12";
            request.PaymentDealerRequest.CvcNumber = "123";
            request.PaymentDealerRequest.Amount = 0.1m;
            request.PaymentDealerRequest.Currency = "TL";
            request.PaymentDealerRequest.InstallmentNumber = 1;
            request.PaymentDealerRequest.ClientIP = "10.10.10.10";
            request.PaymentDealerRequest.RedirectUrl = "https://www.abcasdf.com/PayResult?MyTrxId=123456";

            string postUrl = "https://service.moka.com/PaymentDealer/DoDirectPaymentThreeD";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(postUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(request);
                streamWriter.Write(json);
            }

            DealerPaymentServicePaymentResult dealerPaymentServicePaymentResult;

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                dealerPaymentServicePaymentResult = new JavaScriptSerializer().Deserialize<DealerPaymentServicePaymentResult>(result);
            }
            if (dealerPaymentServicePaymentResult.ResultCode.Equals("Success"))
            {
                string redirectUrl = dealerPaymentServicePaymentResult.Data;
                return redirectUrl;
            }
            else
            {
                if (dealerPaymentServicePaymentResult.ResultCode.Equals("PaymentDealer.DoDirectPayment3dRequest.InstallmentNotAvailableForForeignCurrencyTransaction"))
                {
                    return "Yabancı para işlemlerinde taksit işlemi uygulanamaz!";
                }
                else if (dealerPaymentServicePaymentResult.ResultCode.Equals("PaymentDealer.DoDirectPayment3dRequest.ThisInstallmentNumberNotAvailableForDealer"))
                {
                    return "Seçtiğiniz taksit bayi hesabınızda tanımlı değildir!";
                }
                else if (dealerPaymentServicePaymentResult.ResultCode.Equals("PaymentDealer.DoDirectPayment3dRequest.ForeignCurrencyNotAvailableForThisDealer"))
                {
                    return "Yabancı para işlemleri bayi tanımınızda tanımlı değildir!";
                }
                else if (dealerPaymentServicePaymentResult.ResultCode.Equals("PaymentDealer.DoDirectPayment3dRequest.ThisInstallmentNumberNotAvailableForVirtualPos"))
                {
                    return "Bu taksit sayısı seçili sanal pos için kullanılamaz!";
                }
                else if (dealerPaymentServicePaymentResult.ResultCode.Equals("PaymentDealer.CheckDealerPaymentLimits.DailyDealerLimitExceeded"))
                {
                    return "Bayi limit aşımı nedeniyle işleminizi gerçekleştiremiyoruz. Lütfen ilgili birimimizle irtibata geçiniz.";
                }
                return "";
            }

        }
    }
}
