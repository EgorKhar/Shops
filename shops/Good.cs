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
    
    public partial class Good
    {
        public Good()
        {
            this.Cash = new HashSet<Cash>();
        }
    
        public int Id { get; set; }
        public string Article { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public Nullable<int> Price { get; set; }
    
        public virtual ICollection<Cash> Cash { get; set; }
    }
}
