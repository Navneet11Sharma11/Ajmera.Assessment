using Ajmera.Assessment.Shared.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajmera.Assessment.Shared.FluentValidators
{
    public class BookMasterResponseDtoValidator : AbstractValidator<BookMasterResponseDto>
    {
        public BookMasterResponseDtoValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.AuthorName).NotEmpty();

            RuleFor(t => t.Name).NotEqual("string").WithMessage("Enter valid value for {PropertyName}");
            RuleFor(t => t.AuthorName).NotEqual("string").WithMessage("Enter valid value for {PropertyName}");
        }
    }

    public class BookMasterRequestDtoValidator : AbstractValidator<BookMasterRequestDto>
    {
        public BookMasterRequestDtoValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.AuthorName).NotEmpty();

            RuleFor(t => t.Name).NotEqual("string").WithMessage("Enter valid value for {PropertyName}");
            RuleFor(t => t.AuthorName).NotEqual("string").WithMessage("Enter valid value for {PropertyName}");
        }
    }
}
