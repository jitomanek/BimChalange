using Bim.Core.Entity;
using Bim.Core.Repositories;
using Bim.Core.Services;
using Bim.Core.Services.Interface;

using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Bim;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    var startup = new Startup();
    startup.ConfigureServices(builder.Services, builder.Configuration);


    //Application Services
    builder.Services.AddDbContext<BimContext>(c => c.UseInMemoryDatabase("BimDatabase"));

    builder.Services.AddTransient<ITaskService, TaskService>();

    builder.Services.AddTransient<TaskRepository>();


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

}
catch (Exception e)
{
    logger.Error(e, "Program stopped due to bad configuration");
}