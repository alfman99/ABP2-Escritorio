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

        public void HacerTodo(ref String mensaje)
        {
            mensaje = "";

            List<DIES_SETMANA> diasDeLaSemana = BD.ORM_DIES_SETMANA.SelectAllDIES_SETMANA(ref mensaje);

            if (mensaje.Equals(""))
            {
                foreach (var hora in _espacio.INSTALACIONS.HORARIS_INSTALACIONS)
                {
                    switch (hora.id_dia_setmana)
                    {
                        case 1:
                            Lunes = "LUNES";
                            break;
                        case 2:
                            Martes = "MARTES";
                            break;
                        case 3:
                            Miercoles = "MIERCOLES";
                            break;
                        case 4:
                            Jueves = "JUEVBES";
                            break;
                        case 5:
                            Viernes = "VIERNES";
                            break;
                        case 6:
                            Sabado = "SABADO";
                            break;
                        case 7:
                            Domingo = "DMINGO";
                            break;
                    }
                }
            }
        }
    }
}
