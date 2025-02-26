using Asp.Versioning;
using AspNetCoreRateLimit;
using EmployeePortal.Data.DataContext;
using EmployeePortal.Data.Interface;
using EmployeePortal.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Extensions.Logging;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(policyBuilder => policyBuilder.AddDefaultPolicy(policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Begin-Versioning
builder.Services.AddApiVersioning(option =>
{

    option.AssumeDefaultVersionWhenUnspecified = true; //This ensures if client doesn't specify an API version. The default version should be considered. 
    option.DefaultApiVersion = new ApiVersion(1, 0); //This we set the default API version
    option.ReportApiVersions = true; //The allow the API Version information to be reported in the client  in the response header. This will be useful for the client to understand the version of the API they are interacting with.

    //------------------------------------------------//
    //This says how the API version should be read from the client's request, 3 options are enabled 1.Querystring, 2.Header, 3.MediaType. 
    //"api-version", "X-Version" and "ver" are parameter name to be set with version number in client before request the endpoints.
    option.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));


}).AddMvc() // This is needed for controllers
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
}); ;

//End - Versioning


var connectionString = builder.Configuration.GetConnectionString("EmployeeDB");
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(connectionString));

//Begin - JWT Token
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = jwtIssuer,
           ValidAudience = jwtIssuer,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
       };
   });

//Begin - JWT Token

// Configure NLog
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
});

//Begin RateLimit
//builder.Services.Configure<IpRateLimitOptions>(options =>
//{
//    options.EnableEndpointRateLimiting = true;
//    options.StackBlockedRequests = false;
//    options.HttpStatusCode = 429;
//    options.RealIpHeader = "X-Real-IP";
//    options.ClientIdHeader = "X-ClientId";
//    options.GeneralRules = new List<RateLimitRule>
//        {
//            new RateLimitRule
//            {
//                Endpoint = "GET:/employee/getAllEmployees",
//                Period = "10s",
//                Limit = 2,
//            }
//        };
//});

//End - RateLimit

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
// Add NLog as the logger provider
builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();
//builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
//builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
//builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
//builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
//builder.Services.AddInMemoryRateLimiting();

//Begin - Caching
builder.Services.AddMemoryCache();
//End - Caching

var app = builder.Build();

app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseIpRateLimiting();
// Add .NET JWT authentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
