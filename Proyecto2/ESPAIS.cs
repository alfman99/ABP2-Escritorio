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
    
    public partial class ESPAIS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESPAIS()
        {
            this.ACTIVITATS = new HashSet<ACTIVITATS>();
            this.ACTIVITATS_DEMANADES = new HashSet<ACTIVITATS_DEMANADES>();
        }
    
        public int id { get; set; }
        public string nom { get; set; }
        public double preu { get; set; }
        public bool exterior { get; set; }
        public int id_instalacio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVITATS> ACTIVITATS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVITATS_DEMANADES> ACTIVITATS_DEMANADES { get; set; }
        public virtual INSTALACIONS INSTALACIONS { get; set; }
    }
}
