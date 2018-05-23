namespace eBike.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vendor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendor()
        {
            Parts = new HashSet<Part>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public int VendorID { get; set; }

        [Required(ErrorMessage ="Vendor name is required")]
        [StringLength(100, ErrorMessage = "Vendor name is limited to 100 characters")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(12, ErrorMessage = "Phone number is limited to 12 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(30, ErrorMessage = "Address is limited to 30 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage ="City is required")]
        [StringLength(30, ErrorMessage = "City cant be longer than 30 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province ID is required")]
        [StringLength(2, ErrorMessage = "Province cant be longer than 2 characters")]
        public string ProvinceID { get; set; }

        [Required(ErrorMessage ="Postal code is required")]
        [StringLength(6, ErrorMessage = "Postal Code cant be longer than 6 characters")]
        public string PostalCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Part> Parts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
