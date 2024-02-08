using System.Text.Json;

namespace Samurai_V2_.Net_8.CommonFile
{
    public class CustomeMiddleWareService
    {
        public CustomeMiddleWareService(long ErrorCode, string ErrorMessage, string ErrorDetails = null)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
            this.ErrorDetails = ErrorDetails;
        }
        public long ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
