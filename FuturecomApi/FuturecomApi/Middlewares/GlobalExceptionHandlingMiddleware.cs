using System;
using System.Net;
using System.Text.Json;
using Azure.Core;
using DataAccessLayer.Concrete;
using DataAccessLayer.TokenManager;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuturecomApi.Middlewares
{
	public class GlobalExceptionHandlingMiddleware:IMiddleware
	{


        

        private readonly Context _context;

		public GlobalExceptionHandlingMiddleware(Context context)
        {
          
            _context=context;

        }
        
     
        
		

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {

               

                await next(context);


                   


            }
          
                catch (Exception e)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                   
                    _context.Errors.Add(new Error
                    {
                        Type = "Exception",
                        StatusCode = context.Response.StatusCode.ToString(),
                        Message = e.Message, 
                        CreatedDate = DateTime.UtcNow
                    });
                    await _context.SaveChangesAsync();

                    
                }



           






        }
    }
}


