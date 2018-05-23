namespace eBike.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
            SaleRefunds = new HashSet<SaleRefund>();
        }

        public int SaleID { get; set; }

        public DateTime SaleDate { get; set; }

        [StringLength(128, ErrorMessage ="User name is limited to 128 characters")]
        public string UserName { get; set; }

        public int EmployeeID { get; set; }

        [Column(TypeName = "money")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        public int? CouponID { get; set; }

        [Required(ErrorMessage ="Payment type is required")]
        [StringLength(1, ErrorMessage ="Payment type is limited to 1 character")]
        public string PaymentType { get; set; }

        public Guid? PaymentToken { get; set; }

        public int? JobID { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Job Job { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleRefund> SaleRefunds { get; set; }
    }
}
