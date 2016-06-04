using System.Collections.Generic;

namespace BlogUIWeb.ViewDTO
{
    public class JsonMessage
    {
        public bool Success { get; set; }
        public string ResponseMessage { get; set; }
    }

    public class ErrorJsonMessages : JsonMessage
    { 
        public IList<string> ResponseMessages { get; set; }
    }
}