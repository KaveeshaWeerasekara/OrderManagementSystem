using System.Net;

namespace OrderManagement.DTOs.Responses
{
    public class BaseResponse
    {
        public int status_code { get; set; }
        public object data { get; set; }
        public void CreateResponse(HttpStatusCode statusCode, object data)
        {
            status_code = (int)statusCode;
            this.data = data;
        }
    }
}
