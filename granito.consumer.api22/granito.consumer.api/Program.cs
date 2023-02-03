using granito.bootstrapper.Configurations.AutoMapper;
using granito.bootstrapper.Configurations.Cors;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var provider = services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddAutoMapperConfiguration();
services.AddAutoMapperModelViewConfiguration();

LoggerBuilder.ConfigureLogging();

services.AddProtectedControllers();
services.AddServices(configuration);
services.AddCors();
services.AddSwagger();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCorsConfig();
app.UseAuthorization();
app.UseAuthentication();
app.UseRouting();
app.UseSwaggerConfig();
app.UseEndpointsConfig();
app.UseHttpsRedirection();

app.Run();