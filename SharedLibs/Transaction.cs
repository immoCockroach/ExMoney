using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ExMoney.SharedLibs
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        [Required] public double Amount { get; set; } = 0.0;
        [Required] public double Rate { get; set; } = 1.0;
        public TransactionStatus Status { get; set; } = TransactionStatus.NoStatus;

        //Currencies
        public int BaseCurrencyId { get; set; }
        public int ChangeCurrencyId { get; set; }

        //User making the transaction
        [Required] public string UserId { get; set; }
        
        [Required] public string TransactionId { get; set; }
    }

}
