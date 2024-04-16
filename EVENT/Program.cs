using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = "Host=localhost;Port=5432;Database=Events;Username=postgres;Password=11223399";
NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();
builder.Services.AddDbContext<IDataBase, DataBase>(options =>
                options.UseNpgsql(connection), ServiceLifetime.Transient, ServiceLifetime.Transient);


builder.Services.AddScoped<ICalendarsService, CalendarsService>();
builder.Services.AddScoped<IEventsService, EventsService>();
builder.Services.AddScoped<IInviteService, InviteService>();
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(_ => _.AllowAnyOrigin().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();
app.Run();
