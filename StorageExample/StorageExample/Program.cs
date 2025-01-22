using Azure.Storage;
using Azure.Storage.Blobs;
using StorageExample.Services;
using StorageExample.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register
// Create a URI to the blob
var configuration = builder.Configuration;
string url = $"{configuration.GetValue<string>("AzureStorageSettings:DefaultEndpointsProtocol")}://{configuration.GetValue<string>("AzureStorageSettings:AccountName")}.blob.{configuration.GetValue<string>("AzureStorageSettings:EndpointSuffix")}/{configuration.GetValue<string>("AzureStorageSettings:ImageContainer")}";
Uri blobUri = new(url);

// Create StorageSharedKeyCredentials object by reading
StorageSharedKeyCredential storageCredentials = new(configuration.GetValue<string>("AzureStorageSettings:AccountName"), configuration.GetValue<string>("AzureStorageSettings:AccountKey"));
builder.Services.AddSingleton(x => new BlobClient(blobUri, storageCredentials));
builder.Services.AddScoped<IFileService, FileService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
