
using BSynchro.Api.DB;
using BSynchro.Api.Interfaces;
using BSynchro.Api.Services;
using Microsoft.EntityFrameworkCore;
;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MyDb"));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
