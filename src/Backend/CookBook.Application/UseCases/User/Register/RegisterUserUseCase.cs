using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            Validate(request);

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
