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
    
    public partial class HORARIS_ACTIVITAT
    {
        public int id { get; set; }
        public System.TimeSpan hora_inici { get; set; }
        public System.TimeSpan hora_fi { get; set; }
        public int id_activitat { get; set; }
        public int id_dia_setmana { get; set; }
    
        public virtual DIES_SETMANA DIES_SETMANA { get; set; }
        public virtual ACTIVITATS ACTIVITATS { get; set; }
    }
}
