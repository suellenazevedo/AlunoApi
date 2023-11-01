using Generation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("AlunoConnection");

//builder.Services.AddDbContext<AlunoContext>(opts => 
   //opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Conexão com o Banco de dados
if (builder.Configuration["Environment:Start"] == "PROD")
{
    // Conexão com o PostgresSQL - Nuvem

    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("secrets.json");

    var connectionString = builder.Configuration
   .GetConnectionString("ProdConnection");

    builder.Services.AddDbContext<AlunoContext>(options =>
        options.UseNpgsql(connectionString)
    );
}
else
{
    // Conexão com o MySql - Localhost
    var connectionString = builder.Configuration
    .GetConnectionString("AlunoConnection");

    builder.Services.AddDbContext<AlunoContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    );
}

builder.Services.
    AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AlunosAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Swagger Como Página Inicial - Nuvem

if (app.Environment.IsProduction())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger", "Alunos API - v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
