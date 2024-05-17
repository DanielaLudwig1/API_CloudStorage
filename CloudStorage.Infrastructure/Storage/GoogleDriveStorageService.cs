using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;

namespace CloudStorage.Infrastructure.Storage
{
    //ESTE É O ARQUIVO ONDE SERÁ FEITO O PROCESSO DE UPLOAD DE ARQUIVO COM O GOOGLE DRIVE
    public class GoogleDriveStorageService : IStorageService
    {
        //Injeção de Dependência
        private readonly GoogleAuthorizationCodeFlow _authorization;

        public GoogleDriveStorageService(GoogleAuthorizationCodeFlow authorization)
        {
            _authorization = authorization;
        }
        public string Upload(IFormFile file, User user)
        {
            var credential = new UserCredential(_authorization, user.Email, new TokenResponse //Credenciais do usuario
            {
                 AccessToken = user.AccessToken, 
                 RefreshToken = user.RefreshToken 
            });

            var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer
            {
                ApplicationName = "GoogleDriveProject", //nome fixo da aplicação, este é o nome dado ao projeto no Google Cloud > Console, pode ser substituido recebendo por Injeção de Dependência
                HttpClientInitializer = credential,
            });

            var driveFile = new Google.Apis.Drive.v3.Data.File //using indireto
            {
                Name = file.Name, //Nome do arquivo, sendo o mesmo originalmente
                MimeType = file.ContentType, //Tipo do arquivo(docx, png, jpeg etc...)
            };
            var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType);
            command.Fields = "id"; //Garante que o Id seja passado pelo googledrive

            var response = command.Upload(); //Onde fará o upload 

            if (response.Status is not Google.Apis.Upload.UploadStatus.Completed)
                throw new Exception(string.Format("Ocorreu um erro durante o upload do arquivo {0}",response.Exception));

            return command.ResponseBody.Id;
        }
    }
}
