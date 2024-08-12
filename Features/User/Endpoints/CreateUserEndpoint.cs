using Ecommerce.Features.User.Requests;
using Ecommerce.Services;
using FastEndpoints;

namespace Ecommerce.Features.User.Endpoints
{
    public class CreateUserEndpoint : Endpoint<CreateUserRequest>
    {
        private readonly UserService _userService;

        public CreateUserEndpoint(UserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/users/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
        {
            var user = new Ecommerce.Models.Entities.User // per risolvere conflitto tra il namespace e il tipo.
            {
                Username = req.Username,
                Email = req.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(req.Password),
                Roles = new List<string> { req.IsAdmin ? "Admin" : "User" } // Aggiungi il ruolo in base alla richiesta
            };

            await _userService.CreateAsync(user);

            await SendAsync(user);
        }
    }
}
