using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2.BD
{
    class ORM_HORARIS_ACT_DEMANA
    {
        public static List<HORARIS_ACT_DEMANA> SelectAllHORARIS_ACT_DEMANA(ref String mensaje)
        {
            List<HORARIS_ACT_DEMANA> _horaris_act_demana = null;

            try
            {
                _horaris_act_demana = (from p in ORM.bd.HORARIS_ACT_DEMANA
                               select p).ToList();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                mensaje = BD.ORM.MensajeError(sqlEx);
            }
            catch (EntityException ex)
            {
                SqlException sqlEx = (SqlException)ex.InnerException;
                mensaje = BD.ORM.MensajeError(sqlEx);
            }

            return _horaris_act_demana;
        }

        public static String InsertHORARIS_ACT_DEMANA(TimeSpan hora_inici, TimeSpan hora_fi, int id_activitat_demanada, int id_dia_setmana)
        {
            HORARIS_ACT_DEMANA horaris_act_demana = new HORARIS_ACT_DEMANA();

            horaris_act_demana.hora_inici = hora_inici;
            horaris_act_demana.hora_fi = hora_fi;
            horaris_act_demana.id_activitat_demanada = id_activitat_demanada;
            horaris_act_demana.id_dia_setmana = id_dia_setmana;

            ORM.bd.HORARIS_ACT_DEMANA.Add(horaris_act_demana);            

            return ORM.SaveChanges();
        }

        public static void DeleteHORARIS_ACT_DEMANA(HORARIS_ACT_DEMANA horaris_act_demana)
        {
            ORM.bd.HORARIS_ACT_DEMANA.Remove(horaris_act_demana);

            ORM.SaveChanges();
        }

        public static String UpdateHORARIS_ACT_DEMANA(int id, TimeSpan hora_inici, TimeSpan hora_fi, int id_activitat_demanada, int id_dia_setmana)
        {
            HORARIS_ACT_DEMANA horaris_act_demana = ORM.bd.HORARIS_ACT_DEMANA.Find(id);

            horaris_act_demana.hora_inici = hora_inici;
            horaris_act_demana.hora_fi = hora_fi;
            horaris_act_demana.id_activitat_demanada = id_activitat_demanada;
            horaris_act_demana.id_dia_setmana = id_dia_setmana;

            return ORM.SaveChanges();
        }
    }
}
