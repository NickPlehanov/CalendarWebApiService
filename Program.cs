using CalendarWebApiService.Data;
using CalendarWebApiService.Helpers;
using CalendarWebApiService.Models;
using CalendarWebApiService.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(x => { x.Conventions.Add(new IgnoreODataControllersConvention()); }).AddOData((opt) =>
{
    opt.AddRouteComponents("", GetEdmModel())
        .EnableQueryFeatures();
}); ;
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.OperationFilter<ODataQueryParametersFilter>();
});

builder.Services.AddSignalR();

builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<INotesService, NotesService>();
builder.Services.AddScoped<IMessage, MessageService>();
builder.Services.AddHostedService<CalendarWebApiService.Helpers.BackgroundService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapHub<MessageService>("/notifications");

app.Run();

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Notes>("Notes");
    return builder.GetEdmModel();
}
