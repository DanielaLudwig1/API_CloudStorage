using Microsoft.AspNetCore.Http;

namespace CloudStorage.Application.UseCases.Users.UploadPhoto
{
    public interface IUploadPhotoUseCase
    {
        public void Execute(IFormFile file);
    }
}
