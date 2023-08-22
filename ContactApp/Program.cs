using ContactApp.Data.Context;
using ContactApp.Data.Repositories.Base;
using ContactApp.Data.Services;
using ContactApp.Data.Services.Interfaces;
using ContactApp.Data.UnitsOfWork;
using ContactApp.Data.UnitsOfWork.Interfaces;
using ContactApp.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllersWithViews();

// Azure database
var connection = builder.Configuration.GetConnectionString("Azure_ConnectionString");
builder.Services.AddDbContext<ContactAppContext>(options =>
    options.UseSqlServer(connection, o =>
    {
        o.EnableRetryOnFailure();
    }));

// Services
builder.Services.AddScoped<IContactService, ContactService>();

// Repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Units Of Work
builder.Services.AddScoped<IContactUnitOfWork, ContactUnitOfWork>();

#region Identity

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login";
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ContactAppContext>()
                .AddDefaultTokenProviders();

// User needs to confirm email to access
builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequiredLength = 8;
    opts.Password.RequireLowercase = true;
    opts.SignIn.RequireConfirmedEmail = true;
});

// Token for reset password
builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(5));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
