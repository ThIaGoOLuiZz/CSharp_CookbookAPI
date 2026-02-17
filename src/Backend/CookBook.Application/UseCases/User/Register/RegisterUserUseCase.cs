using AutoMapper;
using CookBook.Application.Services.AutoMapper;
using CookBook.Application.Services.Cryptography;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.Repository;
using CookBook.Domain.Repository.User;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
using Microsoft.Extensions.Logging.Abstractions;
using System.Reflection;

namespace CookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _automapper;
        private readonly PasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, IUserReadOnlyRepository readOnlyRepository, IUnitOfWork unitOfWork, IMapper automapper, PasswordEncripter passwordEncripter)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _unitOfWork = unitOfWork;
            _automapper = automapper;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _automapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);

            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }
        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExists = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

            if (emailExists)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage);

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
