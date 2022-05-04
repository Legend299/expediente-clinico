using ClienteWeb;

var builder = WebApplication.CreateBuilder(args);

// Ipv4 Default, PORT:8889
/*builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(8899);
    //options.ListenAnyIP(80);
});*/


builder.WebHost.UseIISIntegration();

// Add HttpClient
builder.Services.AddHttpClient("api", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("http://192.168.1.69:8893/api");
});

// Add services to the container.
// DateOnly converter
builder.Services
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

//var Prueba = builder.Configuration["ConexionApi:conexionPrivada"];
//builder.Configuration.GetSection("ConexionApi").Get<ConexionApi>();

builder.Services.AddScoped<ConexionApi>();
builder.Services.Configure<ConexionApi>(builder.Configuration.GetSection("ConexionApi"));

builder.Services.AddControllersWithViews();
builder.Services.AddOptions();

// Use Session
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Page error
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404 || context.Response.StatusCode == 400) {
        context.Request.Path = "/Inicio/Index";
        await next();
    }
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Use session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();
