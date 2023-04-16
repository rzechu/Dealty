namespace Dealty.Shared.Interfaces
{
    public interface IJWTToken
    {
        public string JWTIssuer { get; set; }
        public string JWTAudience { get; set; }
        public byte[] JWTKey { get; set; }
    }
}