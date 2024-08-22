using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GuestBookApp.Data;
using GuestBookApp.Repositories;
using GuestBookApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавление DbContext с использованием SQLite
builder.Services.AddDbContext<GuestBookContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация репозиториев
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Message>, Repository<Message>>();

// Добавление Razor Pages
builder.Services.AddRazorPages();

// Добавление сессий
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Установите время ожидания
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Конфигурация HTTP конвейера
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Использование сессий
app.UseSession();

app.MapRazorPages();

app.Run();
