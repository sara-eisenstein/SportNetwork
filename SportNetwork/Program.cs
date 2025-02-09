using Microsoft.EntityFrameworkCore.Storage;
using Mock;
using Repositorys.Interface;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IContext, DataBase>();
builder.Services.AddServiceExtension();

builder.Services.AddAuthentication(JwtBearerDefaults.)

//TODO להוסיף פה לפי המורה מה שצריך לטוקן

//enable cors
var MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
//enable cors

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//enable cors
app.UseCors(MyAllowSpecificOrigins);
//enable cors

app.MapControllers();

app.Run();
