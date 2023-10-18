using Contract.Service.EmailServices;
using Entity.Models.Email;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using PosSystem.Extensions;
using Serilog;
using Service;

var builder = WebApplication.CreateBuilder(args);

// SeriLogging Service
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
.Services.BuildServiceProvider()
.GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
.OfType<NewtonsoftJsonPatchInputFormatter>().First();


// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureRepositoryManager(); // Repository DI
builder.Services.ConfigureServiceManager(); // Service DI
builder.Services.ConfigureSqlContext(builder.Configuration); // DbContext
builder.Services.ConfigureUploadImageService(); // ImageUpload
builder.Services.ConfigureJWT(builder.Configuration); // JWT
builder.Services.AddAuthentication();// authen
builder.Services.ConfigureIdentity();// Identity
builder.Services.AddEmailServiceConfiguration(builder.Configuration); // Email Service
builder.Services.RequiredEmaiConfiguration(); // Required Email

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddXmlDataContractSerializerFormatters()

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
