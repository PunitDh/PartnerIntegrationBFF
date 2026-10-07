using Microsoft.EntityFrameworkCore;
using PartnerIntegrationBFF.API.Data;
using PartnerIntegrationBFF.API.Repositories;
using PartnerIntegrationBFF.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICurrencyValidator, CurrencyValidator>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();