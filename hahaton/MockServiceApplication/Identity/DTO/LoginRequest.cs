using System.ComponentModel.DataAnnotations;

namespace MockServiceApplication.Identity.DTO;

public record LoginRequest(
    [Required]string Email,
    [Required]string Password);