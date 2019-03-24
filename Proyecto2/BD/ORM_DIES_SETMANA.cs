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
    class ORM_DIES_SETMANA
    {
        public static List<DIES_SETMANA> SelectAllDIES_SETMANA(ref String mensaje)
        {
            List<DIES_SETMANA> _dies_setmana = null;

            try
            {
                _dies_setmana = (from p in ORM.bd.DIES_SETMANA
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

            return _dies_setmana;
        }

        public static String InsertDIES_SETMANA(String nom)
        {
            DIES_SETMANA dia_setmana = new DIES_SETMANA();

            dia_setmana.nom = nom;

            ORM.bd.DIES_SETMANA.Add(dia_setmana);

            return ORM.SaveChanges();
        }

        public static void DeleteDIES_SETMANA(DIES_SETMANA dies_setmana)
        {
            ORM.bd.DIES_SETMANA.Remove(dies_setmana);

            ORM.SaveChanges();
        }

        public static String UpdateDIES_SETMANA(int id, String nom)
        {
            DIES_SETMANA categoria = ORM.bd.DIES_SETMANA.Find(id);

            categoria.nom = nom;

            return ORM.SaveChanges();
        }
    }
}
