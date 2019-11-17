using Pretty.Core.Configuration;

namespace Pretty.Core.Domain.Securities
{
    public class SecuritySettings: ISettings
    {
        /// <summary>
        /// Gets or sets an encryption key
        /// </summary>
        public string EncryptionKey { get; set; }
        public string HashAlgorithm { get; set; } = "SHA1";
    }
}
