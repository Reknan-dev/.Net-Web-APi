using Ecommerce.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Features.User.Endpoints;


public class DeleteUserEndpoint : EndpointWithoutRequest
{
    private readonly UserService _userService;

    public DeleteUserEndpoint(UserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {

        Delete("/users/{id}");
        AllowAnonymous();

    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");
        await _userService.RemoveAsync(id);
        await SendOkAsync();
    }
}
