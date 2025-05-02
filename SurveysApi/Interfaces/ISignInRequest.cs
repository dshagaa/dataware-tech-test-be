
namespace SurveysApi.Interfaces
{
    public interface ILSignInRequest
    {
        string Username { get; set; }
        string Password { get; set; }
    }
    public class SignInRequest : ILSignInRequest
    {
        public SignInRequest(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new BadHttpRequestException("Username cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new BadHttpRequestException("Password cannot be null or empty.");
            }
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}