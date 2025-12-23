using Application.DTOs._auth.Request;
using Application.DTOs._auth.Response;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features._auth.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountService _accountService;

        public AuthenticateUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var authRequest = new AuthenticationRequest
            {
                Email = request.Email,
                Password = request.Password
            };

            return await _accountService.AuthenticateAsync(authRequest, request.IpAddress);
        }
    }
}
