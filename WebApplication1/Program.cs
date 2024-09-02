using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TransactionSystem.DataBaseContext;
using TransactionSystem.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enables enum string conversion
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Makes JSON deserialization case-insensitive
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer("Server=SANDHYAKAJUGAAD\\SQLEXPRESS;Database=transaction;Trusted_Connection=True"));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{ builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("corsapp");
app.MapControllers();

app.Run();
