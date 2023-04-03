using Bim.Core.Entity;
using Bim.Core.Repositories;
using Bim.Core.Services;
using Bim.Core.Services.Interface;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
               {
                   opt.UseAllOfForInheritance();
               })
               .AddSwaggerGenNewtonsoftSupport();

//builder.Services.AddNewtonsoftJson(opt =>
//{
//    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver { NamingStrategy = new CamelCaseNamingStrategy { ProcessDictionaryKeys = false } };
//    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
//    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
//});





builder.Services.AddDbContext<BimContext>(c=>c.UseInMemoryDatabase("BimDatabase"));

builder.Services.AddTransient<ITaskService, TaskService>();

builder.Services.AddTransient<TaskRepository>();


builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", corsBuilder => corsBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(builder.Configuration.GetSection("CorsOrigins").Get<string[]>()));
});

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
