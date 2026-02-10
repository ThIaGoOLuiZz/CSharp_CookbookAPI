using CookBook.Application.Services.AutoMapper;
using CookBook.Application.Services.Cryptography;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.Repository.User;
using CookBook.Exceptions.ExceptionsBase;
using Microsoft.Extensions.Logging.Abstractions;

namespace CookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            var criptografiaSenha = new PasswordEncripter();
            var automapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }, NullLoggerFactory.Instance).CreateMapper();

            Validate(request);

            var user = automapper.Map<Domain.Entities.User>(request);
            user.Password = criptografiaSenha.Encrypt(request.Password);

            await _writeOnlyRepository.Add(user);

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
