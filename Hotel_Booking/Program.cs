using Hotel_Booking.Data;
using Hotel_Booking.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HotelBookingDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotelBookingDBContext")));
builder.Services.AddDbContext<HotelBookingAuthDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotelBookingAuthDBContext")));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRoomRepository, SQLRoomRepository>();
builder.Services.AddScoped<IHotelRepository, SQLHotelRepository>();
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("HotelBokking")
                .AddEntityFrameworkStores<HotelBookingAuthDBContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateIssuer = true,
                                        ValidateAudience = true,
                                        ValidateLifetime = true,
                                        ValidateIssuerSigningKey = true,
                                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                                        ValidAudience = builder.Configuration["Jwt:Audience"],
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                                    });

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
