using webservice2.RabbitMQ.Consumidor;

var builder = WebApplication.CreateBuilder(args);

//
/*
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(Convert.ToInt16(builder.Configuration.GetValue<string>("Puerto:Default")));
});
*/

builder.WebHost.UseIISIntegration();

// Rabbitmq
builder.Services.AddScoped<IConsumidor, Consumidor>();
//builder.Services.AddScoped<IProductor, Productor>();

// Add services to the container.

builder.Services.AddControllers();

//Cors
var domainPrivada = builder.Configuration.GetValue<string>("DomainUrl:WebLocal");
var domainPublica = builder.Configuration.GetValue<string>("DomainUrl:WebPublica");

// Cors temp fix

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCors", builder =>
    {
        //builder.WithOrigins(domainPrivada, domainPublica)
        builder.AllowAnyOrigin()
        //.AllowCredentials()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("MyCors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
