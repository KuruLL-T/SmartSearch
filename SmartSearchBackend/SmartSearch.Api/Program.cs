using SmartSearch.Api.Services;
using SmartSearch.Domain.SearchItemModel;
using SmartSearch.Domain.SearchItemTypeModel;
using SmartSearch.Domain.ServiceModel;
using SmartSearch.Domain.UserModel;
using SmartSearch.Infrastructure;
using SmartSearch.Infrastructure.ManticoreProvider;
using SmartSearch.Infrastructure.SearchItemModel;
using SmartSearch.Infrastructure.SearchItemTypeModel;
using SmartSearch.Infrastructure.ServiceModel;
using SmartSearch.Infrastructure.UserModel;

var builder = WebApplication.CreateBuilder(args);

DbInitializer.Initialize(builder.Configuration);
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IManticoreClientProvider, ManticoreClientProvider>();
builder.Services.AddSingleton<ISearchItemRepository, SearchItemRepository>();
builder.Services.AddSingleton<IServiceRepository, ServiceRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ISearchItemTypeRepository, SearchItemTypeRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "localhost:6379";
    options.InstanceName = "local";
});

builder.Services.AddSingleton<RedisService>();
builder.Services.AddSingleton<ImageService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Регистрация MessageHandler и RabbitMqBackgroundService
builder.Services.AddSingleton<MessageHandler>();
builder.Services.AddHostedService<RabbitMqBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
