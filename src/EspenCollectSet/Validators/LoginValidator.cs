namespace EspenCollectSet.Validators
{
    using FluentValidation;

    [Orc.FluentValidation.ValidatorDescription("tag")]
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(viewModel => viewModel.Username)
                .NotNull().NotEmpty().WithMessage("Please, provide the user name");

            RuleFor(viewModel => viewModel.Password)
                .NotNull().NotEmpty().WithMessage("Please, provide the password");
        }
    }
}
