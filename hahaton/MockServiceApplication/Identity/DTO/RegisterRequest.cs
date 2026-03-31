using System.ComponentModel.DataAnnotations;

namespace MockServiceApplication.Identity.DTO;

public record RegisterRequest(
    [Required]string Email,
    [Required]string Password);