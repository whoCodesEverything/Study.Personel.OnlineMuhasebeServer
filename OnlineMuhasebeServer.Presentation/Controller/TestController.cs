using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineMuhasebeServer.Presentation.Abstraction;

namespace OnlineMuhasebeServer.Presentation.Controller
{
	public sealed class TestController:ApiController
	{
		public TestController(IMediator mediator) : base(mediator)
		{
		}
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("İşlem başarılı");
		}

	}
}
