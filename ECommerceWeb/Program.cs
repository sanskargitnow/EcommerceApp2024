using ECommerceWeb.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddHttpClient();

//builder.Services.AddHttpClient("MyApiClient", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7164");  // Set base URL for API calls
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});

builder.Services.AddHttpClient<ApiServices>("MyApiClient", (t) =>
{
    t.BaseAddress = new Uri("https://localhost:7189");
});

// Ensure configuration is available for DI
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
