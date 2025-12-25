using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Ahora es Guid para consistencia

        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;

        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; } // Auditoría específica de seguridad (IP)

        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }

        // Propiedad calculada: El token está activo si no ha expirado Y no ha sido revocado
        public bool IsActive => Revoked == null && !IsExpired;
    }
}