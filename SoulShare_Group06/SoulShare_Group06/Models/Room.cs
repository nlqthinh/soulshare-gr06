namespace SoulShare_Group06.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Room")]
    public partial class Room
    {
        [Key]
        public int room_id { get; set; }

        public int? user1 { get; set; }

        public int? user2 { get; set; }

        public int? room_status { get; set; }
    }
}
