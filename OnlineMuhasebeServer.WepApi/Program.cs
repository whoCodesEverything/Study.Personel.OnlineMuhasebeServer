using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Application.Services.CompanyServices;
using OnlineMuhasebeServer.Domain;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;
using OnlineMuhasebeServer.Domain.Repositories.UCAFRepositories;
using OnlineMuhasebeServer.Persistance;
using OnlineMuhasebeServer.Persistance.Context;
using OnlineMuhasebeServer.Persistance.Repositories.UCAFRepositories;
using OnlineMuhasebeServer.Persistance.Services.AppServices;
using OnlineMuhasebeServer.Persistance.Services.CompanyServices;
using OnlineMuhasebeServer.WepApi.Configuration;
using OnlineMuhasebeServer.WepApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// Add services to the container.
//builder.Services.AddMediatR(typeof(OnlineMuhasebeServer.Application.AssemblyReference).Assembly);


builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IUCAFCommandRepository, UCAFCommandRepository>();
builder.Services.AddScoped<IUCAFQueryRepository, UCAFQueryRepository>();
builder.Services.AddScoped<IContextService, ContextService>();
builder.Services.AddScoped<IUCAFService, UCAFService>();
// Program.cs içerisine eklenmeli:
builder.Services.AddMediatR(cfg => {
	cfg.RegisterServicesFromAssembly(typeof(OnlineMuhasebeServer.Application.AssemblyReference).Assembly);
});
//builder.Services.InstallServices(builder.Configuration, typeof(IServiceInstaller).Assembly);

builder.Services.InstallServices(
	builder.Configuration,
	typeof(Program).Assembly,                                   
	OnlineMuhasebeServer.Application.AssemblyReference.Assembly
	
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using(var scoped = app.Services.CreateScope())
{
	var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
	if (!userManager.Users.Any())
	{
		userManager.CreateAsync(new AppUser
		{
			UserName="tsaydam",
			Email="tsaydam@gmail.com",
			Id=Guid.NewGuid().ToString(),
			NameLastName="Taner Saydam"
		},"Password12*").Wait();
	}
}

app.Run();
