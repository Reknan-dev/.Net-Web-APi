using Ecommerce.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Features.User.Endpoints
{
    [Authorize(Roles = "Admin")]
    public class GetAllUsersEndpoint : EndpointWithoutRequest
    {
        private readonly UserService _userService;

        public GetAllUsersEndpoint(UserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/users/getall");
            
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var users = await _userService.GetAsync();
            await SendAsync(users);
        }
    }
}
