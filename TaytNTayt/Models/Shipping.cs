//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaytNTayt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shipping
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipping()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public System.Guid ID { get; set; }
        public System.Guid StockID { get; set; }
        public System.Guid InvoiceAdressID { get; set; }
        public System.Guid ShippingAddressID { get; set; }
        public int PaymentTypeID { get; set; }
        public System.DateTime ExpireApprovedate { get; set; }
        public bool IsApproved { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Address Address1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
