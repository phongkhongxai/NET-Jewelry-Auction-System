using Panacea_GroupProject;
using Service;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUserService, UserService>(); 
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IJewelryService, JewelryService>();
builder.Services.AddScoped<IAuctionRequestService, AuctionRequestService>();
builder.Services.AddScoped<IMaterialService, MaterialService>(); 
builder.Services.AddScoped<IBidService, BidService>(); 
builder.Services.AddScoped<IUserAuctionService, UserAuctionService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();


builder.Services.AddSignalR();
builder.Services.AddDistributedMemoryCache();


builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "CookieAuth";
    options.LoginPath = "/Accounts/Login";
    options.AccessDeniedPath = "/Accounts/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("MemberOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Member"));
});


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapHub<AuctionHub>("/auctionHub");

app.MapRazorPages();

app.Run();
