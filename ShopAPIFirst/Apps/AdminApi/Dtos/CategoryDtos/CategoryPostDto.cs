using FluentValidation;
using Microsoft.AspNetCore.Http;
using ShopAPIFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPIFirst.Apps.AdminApi.Dtos.CategoryDtos
{
    public class CategoryPostDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }

    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(20).WithMessage("Name field can not be longer than 20 characters!")
                .NotEmpty().WithMessage("Name is required!");
            RuleFor(x => x.Image).Custom((x, content) =>
            {
                if (!x.IsImage())
                {
                    content.AddFailure("ImageFile", "Please insert a valid image type such as jpg,png,jpeg etc");
                }
                if (!x.IsSizeOkay(2))
                {
                    content.AddFailure("ImageFile", "Image size can not be more than 2MB");
                }

            }).NotNull();
            
        }
    }
}
