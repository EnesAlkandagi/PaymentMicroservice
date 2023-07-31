using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaymentMicroService.Business.Concrete;
using PaymentMicroService.Entities.Concrete;

namespace PaymentMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        [HttpPost("payment3D")]
        public IActionResult Payment3D()
        {
            Payment payment = new Payment();
            var result =payment.GetPayment();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult PayResult(bool isSuccessful, string resultCode, string resultMessage, string trxCode)
        {
            Payment payment = new Payment();
            var result = payment.GetPayment();
            if (isSuccessful)
            {
                string SuccessMessage = "Ödeme başarıyla tamamlandı! Trx Code: " + trxCode;
            }
            else
            {
                string ErrorMessage = "Ödeme sırasında bir hata oluştu! ResultCode: " + resultCode + ", Result Message: " + resultMessage + "";
            }

            return Redirect("/DealerPayment/Pay3d");
        }
    }

}
