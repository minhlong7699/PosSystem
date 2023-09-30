using Microsoft.AspNetCore.HttpOverrides;
using PosSystem.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// SeriLogging Service
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureRepositoryManager(); // Repository DI
builder.Services.ConfigureServiceManager(); // Service DI
builder.Services.ConfigureSqlContext(builder.Configuration); // DbContext
builder.Services.ConfigureWebRootPath(); // WebRootProvider
builder.Services.ConfigureUploadImageService(); // ImageUpload

builder.Services.AddControllers()
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly); // Config Controller Assembly
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program)); // AutoMapper Configuration

var app = builder.Build();

var logger = app.Services.GetRequiredService<Serilog.ILogger>();
app.ConfigureExceptionHandler(logger);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
