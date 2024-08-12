using Ecommerce.Features.Requests;
using Ecommerce.Models.Entities;
using Ecommerce.Services;
using FastEndpoints;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class LoginEndpoint : Endpoint<LoginRequest>
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public LoginEndpoint(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
    }

    public string GenerateJwtToken(User user, IConfiguration configuration)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

        // Aggiungi i ruoli come claims
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = (await _userService.GetAsync()).FirstOrDefault(u => u.Username == req.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
        {
            await SendUnauthorizedAsync();
            return;
        }

        var token = GenerateJwtToken(user, _configuration);

        await SendAsync(new { Token = token });
    }

    

}