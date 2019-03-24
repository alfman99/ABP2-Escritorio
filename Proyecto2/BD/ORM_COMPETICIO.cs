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
    class ORM_COMPETICIO
    {
        public static List<COMPETICIO> SelectAllCOMPETICIO(ref String mensaje)
        {
            List<COMPETICIO> _competicio = null;

            try
            {
                _competicio = (from p in ORM.bd.COMPETICIO
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


            return _competicio;
        }

        public static String InsertCOMPETICIO(String nom)
        {
            COMPETICIO competicio = new COMPETICIO();

            competicio.nom = nom;

            ORM.bd.COMPETICIO.Add(competicio);

            return ORM.SaveChanges();
        }

        public static void DeleteCOMPETICIO(COMPETICIO competicio)
        {
            ORM.bd.COMPETICIO.Remove(competicio);

            ORM.SaveChanges();
        }

        public static String UpdateCOMPETICIO(int id, String nom)
        {
            COMPETICIO competicio = ORM.bd.COMPETICIO.Find(id);

            competicio.nom = nom;

            return ORM.SaveChanges();
        }
    }
}
