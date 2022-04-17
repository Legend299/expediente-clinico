using ClienteWeb;

var builder = WebApplication.CreateBuilder(args);

//Ipv4 Default, PORT:8891
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(8899);
});

// Add services to the container.
//DateOnly converter
builder.Services
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

//var Prueba = builder.Configuration["ConexionApi:conexionPrivada"];
//builder.Configuration.GetSection("ConexionApi").Get<ConexionApi>();
//builder.Services.AddScoped<ConexionApi>();

builder.Services.Configure<ConexionApi>(builder.Configuration.GetSection("ConexionApi"));
builder.Services.AddControllersWithViews();
builder.Services.AddOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
