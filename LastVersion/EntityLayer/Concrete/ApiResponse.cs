using System;
namespace EntityLayer.Concrete
{
	public class ApiResponse
	{
       
            public bool Success { get; set; }

            public string Message { get; set; }

            public string accessToken { get; set; }

            public string refreshToken { get; set; }

    }
}

