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
    class ORM_HORARIS_ACTIVITAT
    {
        public static List<HORARIS_ACTIVITAT> SelectAllHORARIS_ACTIVITAT(ref String mensaje)
        {
            List<HORARIS_ACTIVITAT> _horaris_activitat = null;

            try
            {
                _horaris_activitat = (from p in ORM.bd.HORARIS_ACTIVITAT
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

            return _horaris_activitat;
        }

        public static String InsertHORARIS_ACTIVITAT(TimeSpan hora_inici, TimeSpan hora_fi, int id_activitat, int id_dia_setmana)
        {
            HORARIS_ACTIVITAT horaris_activitat = new HORARIS_ACTIVITAT();

            horaris_activitat.hora_inici = hora_inici;
            horaris_activitat.hora_fi = hora_fi;
            horaris_activitat.id_activitat = id_activitat;
            horaris_activitat.id_dia_setmana = id_dia_setmana;

            ORM.bd.HORARIS_ACTIVITAT.Add(horaris_activitat);

            return ORM.SaveChanges();
        }

        public static void DeleteHORARIS_ACTIVITAT(HORARIS_ACTIVITAT horaris_activitat)
        {
            ORM.bd.HORARIS_ACTIVITAT.Remove(horaris_activitat);

            ORM.SaveChanges();
        }

        public static String UpdateHORARIS_ACTIVITAT(int id, TimeSpan hora_inici, TimeSpan hora_fi, int id_activitat, int id_dia_setmana)
        {
            HORARIS_ACTIVITAT horaris_activitat = ORM.bd.HORARIS_ACTIVITAT.Find(id);

            horaris_activitat.hora_inici = hora_inici;
            horaris_activitat.hora_fi = hora_fi;
            horaris_activitat.id_activitat = id_activitat;
            horaris_activitat.id_dia_setmana = id_dia_setmana;

            return ORM.SaveChanges();
        }
    }
}
