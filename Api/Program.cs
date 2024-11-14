using ProSoft.Library.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.AddProSoftLibrary()
	.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
	.AddOpenApi();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseProSoftLibrary(logger);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
