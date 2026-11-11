using OnlineMuhasebeServer.Domain.Abstractions;

namespace OnlineMuhasebeServer.Domain.AppEntities.CompanyEntities
{
	public sealed class UniformChartOfAccount:Entity
	{

		public string Code { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }

		//public string CompanyId { get; set; }
	}
}
