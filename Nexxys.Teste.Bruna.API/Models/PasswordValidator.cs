using FluentValidation;
using System.Collections.Generic;

namespace Nexxys.Teste.Bruna.API
{
    public class PasswordRequest
    {
         public string CheckPassword { get; set; }
    }

    public class ParamRequest
    {
        public string Password { get; set; }
    }

    public class ValidPassword
    {
        public bool IsValid { get; set; }
    }

    public class PasswordRequestValidator : AbstractValidator<PasswordRequest>
    {
        public PasswordRequestValidator()
        {
            RuleFor(x => x.CheckPassword).NotEmpty()
                          .NotNull()
                          .MinimumLength(15)
                          .Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
                          .Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
                          .Matches(@"\b(@|#|_|-|!)\b")
                          .WithMessage("'{PropertyName}'  must contain one of the special characters: @, #, !, _, -, .");

        }

    }

    
}
