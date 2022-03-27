using Domain.Models;
using Domain.Rules.Calculator;
using Domain.Rules.CarRules;
using Domain.Rules.DateRules;
using Domain.Rules.FeeRules;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<List<FeeSlot>>(config =>
{
    builder.Configuration.GetSection("FeeSlots")
    .Bind(config);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Congestion Calculator Tax API",
        Description = "Congestion Calculator Tax",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<CalculatorRulesEngine>();
builder.Services.AddScoped<FeeRulesEngine>();
builder.Services.AddScoped<DateRulesEngine>();
builder.Services.AddScoped<CarRulesEngine>();

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
