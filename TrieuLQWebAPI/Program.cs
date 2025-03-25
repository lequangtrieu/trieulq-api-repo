using Net.payOS;
using TrieuLQWebAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PaymentService>();

PayOS payOS = new PayOS(builder.Configuration["PayOsSettings:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
	builder.Configuration["PayOsSettings:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
	builder.Configuration["PayOsSettings:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));

builder.Services.AddSingleton(payOS);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
