using AutoMapper;
using OnlineMuhasebeServer.Application.Features.CompanyFeatures.UCAFFeatures.Commands.CreateUCAF;
using OnlineMuhasebeServer.Application.Services.CompanyServices;
using OnlineMuhasebeServer.Domain;
using OnlineMuhasebeServer.Domain.AppEntities.CompanyEntities;
using OnlineMuhasebeServer.Domain.Repositories.UCAFRepositories;
using OnlineMuhasebeServer.Persistance.Context;

namespace OnlineMuhasebeServer.Persistance.Services.CompanyServices
{
	public sealed class UCAFService : IUCAFService
	{
		private readonly IUCAFCommandRepository _commandRepository;
		private readonly IContextService _contextService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private CompanyDbContext _context;

		public UCAFService(IUCAFCommandRepository commandRepository,IContextService contextService,IUnitOfWork unitOfWork,IMapper mapper)
		{
			_contextService = contextService;
			_commandRepository = commandRepository;
			_unitOfWork = unitOfWork; // <-- Kesinlikle _unitOfWork değişkenine atanmalı!
			_mapper = mapper;
		}

		public async Task CreateUcafAsync(CreateUCAFCommand request, CancellationToken cancellationToken)
		{
			_context = (CompanyDbContext)_contextService.CreateDbContextInstance(request.CompanyId.ToString());
			_commandRepository.SetDbContextInstance(_context);
			_unitOfWork.SetDbContextInstance(_context);
			UniformChartOfAccount uniformChartOfAccount=_mapper.Map<UniformChartOfAccount>(request);
		
			uniformChartOfAccount.Id = Guid.NewGuid();
			await _commandRepository.AddAsync(uniformChartOfAccount, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);


		}
	}
}
