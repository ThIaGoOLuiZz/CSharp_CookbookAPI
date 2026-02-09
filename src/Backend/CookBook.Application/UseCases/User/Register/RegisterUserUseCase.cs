using CookBook.Application.Services.AutoMapper;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Exceptions.ExceptionsBase;
using Microsoft.Extensions.Logging.Abstractions;

namespace CookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            Validate(request);

            var automapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }, NullLoggerFactory.Instance).CreateMapper();

            var user = automapper.Map<Domain.Entities.User>(request);

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }
        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage);

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
