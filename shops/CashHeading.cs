//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace shops
{
    using System;
    using System.Collections.Generic;
    
    public partial class CashHeading
    {
        public CashHeading()
        {
            this.Cash = new HashSet<Cash>();
        }
    
        public int Id { get; set; }
        public int Id_customer { get; set; }
        public int Id_shop { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual ICollection<Cash> Cash { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
