using Ajmera.Assessment.Shared.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajmera.Assessment.Shared.FluentValidators
{
    public class BookMasterDtoValidator : AbstractValidator<BookMasterDto>
    {
        public BookMasterDtoValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.AuthorName).NotEmpty();

            RuleFor(t => t.Name).NotEqual("string").WithMessage("Enter valid value for {PropertyName}");
            RuleFor(t => t.AuthorName).NotEqual("string").WithMessage("Enter valid value for {PropertyName}");
        }
    }
}
