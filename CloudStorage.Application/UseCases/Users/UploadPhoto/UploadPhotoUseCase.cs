using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Storage;
using FileTypeChecker;
using Microsoft.AspNetCore.Http;

namespace CloudStorage.Application.UseCases.Users.UploadPhoto
{
    public class UploadPhotoUseCase : IUploadPhotoUseCase
    {
        //
        private readonly IStorageService _storageService; //Só pode ser lido a atribuido valor o construtor UploadProfilePhotoUseCase que possui a interface

        //Construtor
        public UploadPhotoUseCase(IStorageService storageService) //IStorageService = parâmetro
        {
            _storageService = storageService;
        }

        public void Execute(IFormFile file)
        {
            var streamFile = file.OpenReadStream();

            //VALIDAR VÁRIAS EXTENSÕES DE UM MESMO TIPO
            //ARQUIVOS = IsArchive
            //EXECUTAVEIS = IsExecutable
            //DOCUMENTOS = IsDocument
            var isImage = FileTypeValidator.IsImage(streamFile); //Retorna um Boolean
            //---------//------------
            //VALIDAR APENAS UM TIPO ESPECIFICO
            //var isImage = streamFile.Is<JointPhotographicExpertsGroup>();
            //.PDF = PortableDocumentFormat
            //.PNG = PortableNetworkGraphic

            if (isImage == false)
                throw new Exception("O Arquivo que você tentou enviar não é do tipo Imagem");

            var user = GetFromDataBase(); //Passando os dados do Usuario adquiridos via get

            _storageService.Upload(file, user);

        }

        private User GetFromDataBase() //Simulando get de dados do banco de dados
        {
            return new User
            {
                //Dados fixos
                Id = 1,
                Name = "Danniela",
                Email = "danniela@gmail.com",
                RefreshToken = "1//04A7xgzuGBYVNCgYIARAAGAQSNwF-L9IrvJgHCHyhQXjfvWC9Z7uv55d2witOzoyabmwIKT0eC6KtTRWxiGLJQDItI2kSCzE82Ao",
                AccessToken = "ya29.a0Ad52N3-ajrWeb5bxoVZ3lOaOmddfPKTz04xUguDfRSN2QIfg3RMxaOZf6unqZiBNqzEySwrFvxLUfDmCthdh2fR5xaHKrKqBGDIOlhqOzzAMGqboh7AKqk6QRlthm006gn3uHN9jHXtQtWvQ_qW1NlgtEMsVn1FWCP7HaCgYKAVwSARESFQHGX2Mi2ZivIeVPEIYg9zQr6eZG1g0171",

            };
        }
    }
}
