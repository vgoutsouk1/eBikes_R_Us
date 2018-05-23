namespace eBike.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnorderedPurchaseItemCart")]
    public partial class UnorderedPurchaseItemCart
    {
        [Key]
        public int CartID { get; set; }

        public int PurchaseOrderNumber { get; set; }

        
        [StringLength(100, ErrorMessage = "The length of Description is limited to 100 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Vendor part number is required")]
        [StringLength(50, ErrorMessage ="Vendor part number is limited to 50 characters")]
        public string VendorPartNumber { get; set; }

        public int Quantity { get; set; }
    }
}
