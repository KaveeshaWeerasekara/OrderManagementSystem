using Microsoft.EntityFrameworkCore;
using OrderService;
using OrderService.Interservice.OrderProductService;
//using OrderService.Interservice.OrderService;
using OrderService.Interservice.ProductMicroService;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IOrderService, OrdersService>();
builder.Services.AddHttpClient<IOrderProductServiceClient, OrderProductServiceClient>();
builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>();
//builder.Services.AddHttpClient<IOrderServiceClient, OrderServiceClient>();

/////////////////////////////////////////////

/*builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client =>
{
    client.BaseAddress = new Uri("http://ProductService/api/products/");
});

////////////////////////////////////////////



builder.Services.AddHttpClient<IOrderProductServiceClient, OrderProductServiceClient>(client =>
{
    client.BaseAddress = new Uri("http://OrderProductservice/api/orderProducts/");
});
*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
