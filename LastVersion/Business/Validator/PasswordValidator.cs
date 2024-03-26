using System;
using DtoLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.Validator
{
	public class PasswordValidator:AbstractValidator<UserPwUpdateDto>
	{
        public PasswordValidator()
        {
            RuleFor(x => x.NewPw)
                .NotEmpty().WithMessage("Parola gereklidir.")
                .MinimumLength(8).WithMessage("Parola en az 8 karakter uzunluğunda olmalıdır.")
                .Matches("^(?!.*[0-9]{3,})(?!.*[a-zA-Z]{3,}).*$").WithMessage("Password cannot contain consecutive 3 numbers or 3 consecutive letters");
        }



    }
}

