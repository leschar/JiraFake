using FluentValidation.Results;
using JiraFake.Application.Worker;
using JiraFake.Data.Context;
using JiraFake.Data.Repositories.Models;
using JiraFake.Domain.AppSettings;
using JiraFake.Domain.Commands.SubSubTarefa;
using JiraFake.Domain.Commands.SubTarefa;
using JiraFake.Domain.Commands.Tarefa;
using JiraFake.Domain.Communications.RabbitMq;
using JiraFake.Domain.Communications.RabbitMq.Patterns.Factory;
using JiraFake.Domain.Events.Models.SubTarefa;
using JiraFake.Domain.Events.Models.Tarefa;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Interfaces.Rabbit;
using JiraFake.Domain.Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

builder.Services.AddScoped<JiraFakeContext>();
builder.Services.AddDbContext<JiraFakeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IRequestHandler<AdicionarTarefaCommand, ValidationResult>, TarefaCommandHandler>();
builder.Services.AddScoped<IRequestHandler<EditarTarefaCommand, ValidationResult>, TarefaCommandHandler>();
builder.Services.AddScoped<IRequestHandler<RemoverTarefaCommand, ValidationResult>, TarefaCommandHandler>();

builder.Services.AddScoped<IRequestHandler<AdicionarSubTarefaCommand, ValidationResult>, SubTarefaCommandHandler>();
builder.Services.AddScoped<IRequestHandler<EditarSubTarefaCommand, ValidationResult>, SubTarefaCommandHandler>();
builder.Services.AddScoped<IRequestHandler<RemoverSubTarefaCommand, ValidationResult>, SubTarefaCommandHandler>();


builder.Services.AddScoped<INotificationHandler<AdicionarTarefaEvent>, TarefaEventHandler>();
builder.Services.AddScoped<INotificationHandler<EditarTarefaEvent>, TarefaEventHandler>();
builder.Services.AddScoped<INotificationHandler<RemoverTarefaEvent>, TarefaEventHandler>();

builder.Services.AddScoped<INotificationHandler<AdicionarSubTarefaEvent>, SubTarefaEventHandler>();
builder.Services.AddScoped<INotificationHandler<EditarSubTarefaEvent>, SubTarefaEventHandler>();
builder.Services.AddScoped<INotificationHandler<RemoverSubTarefaEvent>, SubTarefaEventHandler>();

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ISubTarefaRepository, SubTarefaRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Fake Jira",
        Description = "Exemplo de como fornecer a documentação para APIs.",
        Contact = new OpenApiContact() { Name = "Charles", Email = "fake@fake.com" },
        License = new OpenApiLicense() { Name = "MIT License", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});


builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.AddScoped(typeof(RabbitMqSender<>));

builder.Services.AddTransient<RabbitMqSender<AdicionarTarefaEvent>>();
builder.Services.AddTransient<RabbitMqSender<EditarTarefaEvent>>();
builder.Services.AddTransient<RabbitMqSender<RemoverTarefaEvent>>();

builder.Services.AddTransient<RabbitMqSender<AdicionarSubTarefaEvent>>();
builder.Services.AddTransient<RabbitMqSender<EditarSubTarefaEvent>>();
builder.Services.AddTransient<RabbitMqSender<RemoverSubTarefaEvent>>();

builder.Services.AddTransient<IFilaRabbit, TarefaFila>();
builder.Services.AddTransient<IFilaRabbit, SubTarefaFila>();
builder.Services.AddTransient<IFilaFactory, FilaFactory>();
builder.Services.AddHostedService<RabbitMqWorker>();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
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

app.UseCors();


app.Run();
