using Ecommerce.Features.User.Requests;
using Ecommerce.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Features.User.Endpoints
{
    
    public class UpdateUserEndpoint : Endpoint<UpdateUserRequest>
    {
        private readonly UserService _userService;

        public UpdateUserEndpoint(UserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Put("/users");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateUserRequest req, CancellationToken c)
        {
            var user = await _userService.GetAsync(req.Id);
            if (user == null)
            {
                await SendNotFoundAsync();
                return;
            }

            user.Username = req.Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(req.Password);
            user.Email = req.Email;

            await _userService.UpdateAsync(req.Id, user);

            await SendAsync(user);
        }
    }
}
