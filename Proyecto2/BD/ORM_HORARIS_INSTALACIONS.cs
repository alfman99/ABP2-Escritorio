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
    class ORM_HORARIS_INSTALACIONS
    {
        public static List<HORARIS_INSTALACIONS> SelectAllHORARIS_INSTALACIONS(ref String mensaje)
        {
            List<HORARIS_INSTALACIONS> _horaris_instalacions = null;

            try
            {
                _horaris_instalacions = (from p in ORM.bd.HORARIS_INSTALACIONS
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

            return _horaris_instalacions;
        }

        public static String InsertHORARIS_INSTALACIONS(TimeSpan hora_inici, TimeSpan hora_fi, int id_instalacio, int id_dia_setmana)
        {

            HORARIS_INSTALACIONS horari_instalacio = new HORARIS_INSTALACIONS();

            horari_instalacio.hora_inici = hora_inici;
            horari_instalacio.hora_fi = hora_fi;
            horari_instalacio.id_instalacio = id_instalacio;
            horari_instalacio.id_dia_setmana = id_dia_setmana;

            ORM.bd.HORARIS_INSTALACIONS.Add(horari_instalacio);

            return ORM.SaveChanges();
        }

        public static void DeleteHORARIS_INSTALACIONS(HORARIS_INSTALACIONS horari_instalacions)
        {
            ORM.bd.HORARIS_INSTALACIONS.Remove(horari_instalacions);

            ORM.SaveChanges();
        }

        public static String UpdateHORARIS_INSTALACIONS(int id, TimeSpan hora_inici, TimeSpan hora_fi, int id_instalacio, int id_dia_setmana)
        {
            HORARIS_INSTALACIONS horari_instalacio = ORM.bd.HORARIS_INSTALACIONS.Find(id);

            horari_instalacio.hora_inici = hora_inici;
            horari_instalacio.hora_fi = hora_fi;
            horari_instalacio.id_instalacio = id_instalacio;
            horari_instalacio.id_dia_setmana = id_dia_setmana;

            return ORM.SaveChanges();
        }
    }
}
