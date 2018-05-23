namespace eBike.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Jobs = new HashSet<Job>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SaleRefunds = new HashSet<SaleRefund>();
            Sales = new HashSet<Sale>();
        }

        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Social Insurance Number is required")]
        [StringLength(9, ErrorMessage = "SIN cant be longer than 9 characters")]
        public string SocialInsuranceNumber { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(30, ErrorMessage = "Last Name cant be longer than 30 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(30, ErrorMessage = "First Name cant be longer than 30 characters")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Address cant be longer than 40 characters")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "City cant be longer than 20 characters")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Province cant be longer than 2 characters")]
        public string Province { get; set; }

        [StringLength(6, ErrorMessage = "Postal Code cant be longer than 6 characters")]
        public string PostalCode { get; set; }

        [StringLength(12, ErrorMessage = "Home Phone cant be longer than 12 characters")]
        public string HomePhone { get; set; }

        [StringLength(30, ErrorMessage = "Email Address cant be longer than 30 characters")]
        public string EmailAddress { get; set; }

        public int PositionID { get; set; }

        public virtual Position Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Jobs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleRefund> SaleRefunds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
