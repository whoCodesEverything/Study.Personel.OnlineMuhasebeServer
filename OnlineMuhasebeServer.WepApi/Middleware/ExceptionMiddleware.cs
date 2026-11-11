/*using System.ComponentModel.DataAnnotations;

namespace OnlineMuhasebeServer.WepApi.Middleware
{
	public sealed class ExceptionMiddleware:IMiddleware
	{
		public async Task InvokeAsync(HttpContext context,RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex) {

				await HandleExceptionAsync(context, ex);
			}
	}
		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;

		
			if (ex.GetType() == typeof(ValidationException))
			{

				return context.Response.WriteAsync(new ValidationErrorDetails
				{
					Errors=((ValidationException)ex).Errors.Select(s=>s.PropertyName),
					StatusCode=context.Response.StatusCode
				}.ToString());
			}

			return context.Response.WriteAsync(new ErrorResult
			{
				Message=ex.Message,
				StatusCode=context.Response.StatusCode
			}.ToString());
			
		}
		
	}
}
*/

namespace OnlineMuhasebeServer.WepApi.Middleware
{
	public sealed class ExceptionMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;

			// FluentValidation Exception kontrolü
			if (ex is FluentValidation.ValidationException validationException)
			{
				context.Response.StatusCode = StatusCodes.Status400BadRequest;

				return context.Response.WriteAsync(new ValidationErrorDetails
				{
					Errors = validationException.Errors.Select(s => s.PropertyName),
					StatusCode = context.Response.StatusCode
				}.ToString());
			}

			// Diğer genel hatalar için
			return context.Response.WriteAsync(new ErrorResult
			{
				Message = ex.Message,
				StatusCode = context.Response.StatusCode
			}.ToString());
		}
	}
}