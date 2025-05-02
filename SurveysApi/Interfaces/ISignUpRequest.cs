
namespace SurveysApi.Interfaces
{
    public interface ISignUpRequest
    {
        string Username { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public class SignUpRequest : ISignUpRequest
    {
        public SignUpRequest(string username, string password, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(username) || username.Length < 3 || username.Length > 50)
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty and must be between 3 and 50 characters.");
            }
            if (string.IsNullOrEmpty(password) || password.Length < 8 || password.Length > 50 || !password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit) || !password.Any(ch => "!@#$%^&*()_+-=[]{}|;:',.<>?/".Contains(ch)))
            {
                throw new ArgumentNullException(nameof(password), "Password must be between 8 and 50 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            }
            if (string.IsNullOrEmpty(firstName) || firstName.Length < 3 || firstName.Length > 50)
            {
                throw new ArgumentNullException(nameof(firstName), "First name cannot be null or empty and must be between 3 and 50 characters.");
            }
            if (string.IsNullOrEmpty(lastName) || lastName.Length < 3 || lastName.Length > 50)
            {
                throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty and must be between 3 and 50 characters.");
            }

            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}