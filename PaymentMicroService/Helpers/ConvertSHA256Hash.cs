using System.Text;

namespace PaymentMicroService.Helpers
{
    public class ConvertSHA256Hash
    {
        public string SHA256Hash(string hashKey)
        {

            System.Text.Encoding encoding = Encoding.UTF8;
            byte[] plainBytes = encoding.GetBytes(hashKey);
            System.Security.Cryptography.SHA256Managed sha256Engine = new System.Security.Cryptography.SHA256Managed();
            string hashedData = String.Empty;
            byte[] hashedBytes = sha256Engine.ComputeHash(plainBytes, 0, encoding.GetByteCount(hashKey));
            foreach (byte bit in hashedBytes)
            {
                hashedData += bit.ToString("x2");
            }
            return hashedData;
        }
    }
}
