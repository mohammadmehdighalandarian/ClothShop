﻿using System.ComponentModel.DataAnnotations;

namespace ClothShop.DataLayer.Entities.Order
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int OrderSum { get; set; }
        public bool IsFinaly { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }


        public virtual User.User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
