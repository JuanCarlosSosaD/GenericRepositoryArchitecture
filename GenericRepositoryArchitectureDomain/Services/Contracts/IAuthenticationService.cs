namespace Everyware.GRDomain.Services.Contracts;

public interface IAuthenticationService
{
    Task<string> AuthenticateAsync(string name, string email, string id, string consumerProfileId, string individualId, string firstName, string lastName);
}
