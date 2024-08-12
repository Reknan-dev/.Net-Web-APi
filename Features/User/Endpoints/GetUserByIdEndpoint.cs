using Ecommerce.Services;
using FastEndpoints;

namespace Ecommerce.Features.User.Endpoints
{
    public class GetUserByIdEndpoint : EndpointWithoutRequest
    {
        private readonly UserService _userService;

        public GetUserByIdEndpoint(UserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/users/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<string>("id");
            var user = await _userService.GetAsync(id);
            if (user == null)
            {
                await SendNotFoundAsync();
                return;
            }
            await SendAsync(user);
        }
    }
}
