namespace SoulShare_Group06.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [Key]
        public int customer_id { get; set; }

        [Required]
        [StringLength(255)]
        public string customer_name { get; set; }

        [Required]
        [StringLength(255)]
        public string customer_email { get; set; }

        [Required]
        [StringLength(255)]
        public string customer_address { get; set; }

        [Required]
        [StringLength(255)]
        public string account_password { get; set; }

        [Required]
        [StringLength(10)]
        public string account_phone { get; set; }

        public int? customer_role { get; set; }

        public int? gender { get; set; }
    }
}
