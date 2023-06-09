using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs
{
    public class ExMoneySettings
    {
        [Key] public int Id { get; set; }
        [Required] public double CommissionPercentage { get; set; } = 0.05;
        [Required] public string CurrencyExchangeApiKey { get; set; }
        [Url, Required] public string CurrencyEcxhangeBaseUrl { get; set; }
        public bool EmailVerificationEnabled { get; set; } = false;
        public bool IdentityVerificationEnabled { get; set; } = false;
        public bool PhoneVerificationEnabled { get; set; } = false;

        public double LatestN2FRate { get; set; }
        public double LatestF2NRate { get; set; }
    }
}
