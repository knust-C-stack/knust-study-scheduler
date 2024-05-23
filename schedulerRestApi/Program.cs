using schedulerRestApi.Data.Repository;
using schedulerRestApi.Data.Service.Groups;
using schedulerRestApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services for managing groups
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

// Configure CORS
const string AllowBlazorClientPolicy = "AllowBlazorClient";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowBlazorClientPolicy, builder =>
    {
        builder.WithOrigins("http://localhost:5120") // Blazor app URL
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

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
app.UseCors(AllowBlazorClientPolicy); // Enable CORS with the defined policy
app.MapControllers();
app.Run();
