using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApp_vSem3.Abstraction;
using WebApp_vSem3.Data;
using WebApp_vSem3.Graph.Mutation;
using WebApp_vSem3.Graph.Query;
using WebApp_vSem3.Mapper;
using WebApp_vSem3.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddDbContext<WebAppContext>(option => option.UseMySql(builder.Configuration.GetConnectionString("db"), new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>(); 
builder.Services.AddMemoryCache(mc => mc.TrackStatistics = true);


var app = builder.Build();

var staticFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
Directory.CreateDirectory(staticFilePath);
app.UseStaticFiles(new StaticFileOptions { FileProvider = new PhysicalFileProvider(staticFilePath), RequestPath = "/static" });

app.UseHttpsRedirection();

app.MapGraphQL();

app.Run();