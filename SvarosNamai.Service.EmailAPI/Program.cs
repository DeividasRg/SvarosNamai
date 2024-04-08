using Microsoft.EntityFrameworkCore;
using SendGrid;
using SvarosNamai.Serivce.EmailAPI.Service;
using SvarosNamai.Serivce.EmailAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IErrorLogger, ErrorLogger>();

builder.Services.AddScoped<ISendGridClient>(provider =>
{
    return new SendGridClient(builder.Configuration.GetSection("SendGridAPIKey").Get<string>());
});
builder.Services.AddHttpClient("Order", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:OrderAPI"]));
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

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
