using Microsoft.EntityFrameworkCore;
using TestApp.Models.DBManager;
using TestApp.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

JsonManger.SetJeson();
builder.Services.AddDbContextPool<Context>(options => options.UseSqlServer(JsonManger.GetConnectionString()));
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

FCM.ServerKey = JsonManger.GetStringSection("ServerToken");
var context = Context.GetNew();

//await context.Database.EnsureDeletedAsync();

context.Database.Migrate();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();