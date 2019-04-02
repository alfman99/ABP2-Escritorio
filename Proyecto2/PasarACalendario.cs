using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class PasarACalendario
    {
        public ESPAIS _espacio { get; set; }
        public TimeSpan _hora { get; set; }

        public String Lunes { get; set; }
        public String Martes { get; set; }
        public String Miercoles { get; set; }
        public String Jueves { get; set; }
        public String Viernes { get; set; }
        public String Sabado { get; set; }
        public String Domingo { get; set; }

        public PasarACalendario(ESPAIS espacio, TimeSpan hora)
        {
            this._espacio = espacio;
            this._hora = hora;
        }

        public void HacerTodo()
        {
            String mensaje = "";

            List<DIES_SETMANA> diasSemana = BD.ORM_DIES_SETMANA.SelectAllDIES_SETMANA(ref mensaje);
            List<HORARIS_ACTIVITAT> horariosActividades = BD.ORM_HORARIS_ACTIVITAT.SelectAllHORARIS_ACTIVITAT(ref mensaje);

            foreach (var dia in diasSemana)
            {
                foreach (var horarioActividad in horariosActividades)
                {
                    if (horarioActividad.id_dia_setmana == dia.id)
                    {
                        if ((_hora >= horarioActividad.hora_inici) && (_hora <= horarioActividad.hora_fi) && horarioActividad.ACTIVITATS.ACTIVITATS_DEMANADES.assignada)
                        {
                            switch (dia.nom.ToLower())
                            {
                                case "lunes":
                                    Lunes = horarioActividad.ACTIVITATS.nom;
                                    break;
                                case "martes":
                                    Martes = horarioActividad.ACTIVITATS.nom;
                                    break;
                                case "miercoles":
                                    Miercoles = horarioActividad.ACTIVITATS.nom;
                                    break;
                                case "jueves":
                                    Jueves = horarioActividad.ACTIVITATS.nom;
                                    break;
                                case "viernes":
                                    Viernes = horarioActividad.ACTIVITATS.nom;
                                    break;
                                case "sabado":
                                    Sabado = horarioActividad.ACTIVITATS.nom;
                                    break;
                                case "domingo":
                                    Domingo = horarioActividad.ACTIVITATS.nom;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
