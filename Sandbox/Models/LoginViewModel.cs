using System.ComponentModel.DataAnnotations;

namespace Sandbox.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
