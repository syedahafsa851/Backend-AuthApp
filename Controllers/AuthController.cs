using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public AuthController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginModel model)
    {
        var user = await _userRepository.FindUserByEmailAsync(model.Email);
        if (user == null || user.Password != model.Password)
            return BadRequest("Invalid email or password");

        return Ok("Login successful");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _userRepository.FindUserByEmailAsync(model.Email);
        if (existingUser != null)
        {
            return BadRequest("Email is already registered.");
        }

        var newUser = new User
        {
            Email = model.Email,
            Password = model.Password,
        };

        await _userRepository.InsertUserAsync(newUser);

        return Ok("Registration successful");
    }
}

public class UserLoginModel
{
    public string Email { get; set; } 
    public string Password { get; set; }
     public UserLoginModel()
    {
        Email = ""; 
        Password = ""; 
    }
}

public class UserRegistrationModel
{
    public string Email { get; set; }
    public string Password { get; set; }
     public UserRegistrationModel()
    {
        Email = ""; 
        Password = ""; 
    }
}
