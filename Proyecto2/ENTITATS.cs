//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto2
{
    using System;
    using System.Collections.Generic;
    
    public partial class ENTITATS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ENTITATS()
        {
            this.EQUIPS = new HashSet<EQUIPS>();
            this.TELEFONS = new HashSet<TELEFONS>();
        }
    
        public int id { get; set; }
        public string nom { get; set; }
        public string temporada { get; set; }
        public string adreca { get; set; }
        public string NIF { get; set; }
        public string correu { get; set; }
        public string password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EQUIPS> EQUIPS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TELEFONS> TELEFONS { get; set; }
    }
}