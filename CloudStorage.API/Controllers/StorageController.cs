using CloudStorage.Application.UseCases.Users.UploadPhoto;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.API.Controllers
{
    [Route("api/[controller]")] //"Storage" nome do controller
    [ApiController]
    public class StorageController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadImage([FromServices] IUploadPhotoUseCase useCase, IFormFile file)//EndPoint que retornará os codes http
        {
            useCase.Execute(file);

            return Created(); //Criará um Objeto que retornará o código 201
        }  

    }
}
