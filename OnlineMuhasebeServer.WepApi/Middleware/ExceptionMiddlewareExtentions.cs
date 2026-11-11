namespace OnlineMuhasebeServer.WepApi.Middleware
{
	public static class ExceptionMiddlewareExtentions
	{
		public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app) { 
		
		return app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
