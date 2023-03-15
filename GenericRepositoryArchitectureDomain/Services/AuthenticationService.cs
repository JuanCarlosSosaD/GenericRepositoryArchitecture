using Everyware.GRDomain.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Everyware.GRDomain.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly string key;
    private readonly string audience;
    private readonly string issuer;
    private readonly double expire;

    public AuthenticationService(string key, double expire, string issuer, string audience)
    {
        this.key = key;
        this.expire = expire;
        this.issuer = issuer;
        this.audience = audience;
    }

    public async Task<string> AuthenticateAsync(string name, string email, string id, string consumerProfileId, string individualId, string firstName, string lastName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDecriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(expire),
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim("ConsumerId", consumerProfileId),
                new Claim("IndividualId", individualId),
                new Claim("Email", email),
                new Claim("FirstName", firstName),
                new Claim("LastName", lastName),
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(key)),
            SecurityAlgorithms.HmacSha256Signature),
            Audience = audience,
            Issuer = issuer,
        };

        var token = tokenHandler.CreateToken(tokenDecriptor);

        return await Task.Run(() => tokenHandler.WriteToken(token));
    }

}
