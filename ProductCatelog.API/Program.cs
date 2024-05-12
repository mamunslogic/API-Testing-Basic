using ProductCatelog.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "Hello World");
//app.MapPost("/products", () => Results.Ok());
app.MapPost("/products", (ProductCatelogDbContext dbContext, Product product) =>
{
    if (product.Price < 0) return Results.BadRequest();

    dbContext.Products.Add(product);
    dbContext.SaveChanges();
    return Results.Ok();
});

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
