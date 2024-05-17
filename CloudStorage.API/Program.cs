using CloudStorage.Application.UseCases.Users.UploadPhoto;
using CloudStorage.Domain.Storage;
using CloudStorage.Infrastructure.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;

var builder = WebApplication.CreateBuilder(args);

//AQUI É CONFIGURADO AS INJEÇÕES DE DEPENDÊNCIA
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUploadPhotoUseCase, UploadPhotoUseCase>();
builder.Services.AddScoped<IStorageService>(Options => 
{
    var clientId = builder.Configuration.GetValue<string>("CloudStorage:ClientId"); //Adquirindo o ID do cliente do appsettings e passando para variavel clientId
    //Obj: CloudStorage 
    var clientSecret = builder.Configuration.GetValue<string>("CloudStorage:ClientSecret"); //Adquirindo o ID do cliente do appsettings e passando para variavel clientId

    var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
    {
        ClientSecrets = new ClientSecrets
        {
            ClientId = clientId,
            ClientSecret = clientSecret
        },
        Scopes = [Google.Apis.Drive.v3.DriveService.Scope.Drive],
        DataStore = new FileDataStore("GoogleDriveProject")
    });


    return new GoogleDriveStorageService(apiCodeFlow);
});

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
