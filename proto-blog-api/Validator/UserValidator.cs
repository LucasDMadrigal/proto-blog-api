using FluentValidation;
using proto_blog_api.Models;

namespace proto_blog_api.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {
            RuleFor(user => user.Username).NotEmpty();

            //RuleFor(user => user.Password).NotEmpty();
        }
    }
}
