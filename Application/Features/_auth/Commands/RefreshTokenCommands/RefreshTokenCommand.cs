using Application.DTOs._auth.Response;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json.Serialization;

namespace Application.Features._auth.Commands.RefreshTokenCommands
{
    public class RefreshTokenCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public string IpAddress { get; set; }
    }
}
