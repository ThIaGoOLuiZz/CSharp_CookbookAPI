using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Application.UseCases.User.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public IActionResult Register([FromServices]IRegisterUserUseCase useCase, [FromBody]RequestRegisterUserJson request )
        {
            var result = useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
