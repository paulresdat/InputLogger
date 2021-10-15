using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp.Domain.Entities
{
    public class UserInputLog
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserInputLogId { get; set; }

        [StringLength(50)]
        public string UserInput { get; set; }

        [Column(TypeName = "numeric(8,4)")]
        public decimal UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(10)]
        public string CreatedBy { get; set; }
    }
}
