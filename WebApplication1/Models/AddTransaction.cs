using System.ComponentModel.DataAnnotations;
using TransactionSystem.Enums;

namespace TransactionSystem.Models
{
    public class AddTransaction
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        public int RunningBalance { get; set; }
    }
}
