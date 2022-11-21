using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webservice1.Common;
using webservice1.RabbitMQ;
using webservice1.RabbitMQ.Productor;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(Convert.ToInt16(builder.Configuration.GetValue<string>("Puerto:Default")));
});


//builder.WebHost.UseIISIntegration();

// Azure blob
builder.Services.AddScoped(options =>
{
  //return new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=expdocumentos;AccountKey=mHEcVdqPuBCNVqVtRIDWCVYN4brSd4JMW0yRILr368M+CtaS+Ig1VdRieshZD1TaDWT9QBu19UU9rnpVmD4iHg==;EndpointSuffix=core.windows.net");
    return new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=expdocumentos;AccountKey=SLEmY4zec3IVPEKgFaUJhFKiZ/t2bHvLCqJG+6V7tKq3pG0UE5ZHiTFPzcWIZe25x8SVbfmfeWGb+AStFIurrg==;EndpointSuffix=core.windows.net");
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Db connection / Split Query
builder.Services.AddDbContext<expedienteContext>(options => 
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

//DateOnly converter
builder.Services
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

// JWT

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

// token

var appSettings = appSettingsSection.Get<AppSettings>();
var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(d =>
{
    d.RequireHttpsMetadata = false;
    d.SaveToken = true;
    d.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(llave),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IExpedienteService, ExpedienteService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<IDocumentoService, DocumentoService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();

// Rabbitmq
builder.Services.AddScoped<IProductor, Productor>();

//builder.Services.AddControllers();
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Cors
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
