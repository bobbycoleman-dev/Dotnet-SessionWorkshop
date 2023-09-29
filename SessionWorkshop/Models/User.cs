#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace SessionWorkshop.Models;

public class User
{
    [Required(ErrorMessage = "Please enter your name to continue!")]
    public string Name { get; set; }
}