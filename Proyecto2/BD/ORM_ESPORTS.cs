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
    class ORM_ESPORTS
    {
        public static List<ESPORTS> SelectAllESPORTS(ref String mensaje)
        {
            List<ESPORTS> _esports = null;

            try
            {
                _esports = (from p in ORM.bd.ESPORTS
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

            return _esports;
        }

        public static String InsertESPORT(String nom)
        {
            ESPORTS esport = new ESPORTS();

            esport.nom = nom;

            ORM.bd.ESPORTS.Add(esport);

            return ORM.SaveChanges();
        }

        public static void DeleteESPORT(ESPORTS esport)
        {
            ORM.bd.ESPORTS.Remove(esport);

            ORM.SaveChanges();
        }

        public static String UpdateESPORT(int id, String nom)
        {
            ESPORTS esport = ORM.bd.ESPORTS.Find(id);

            esport.nom = nom;

            return ORM.SaveChanges();
        }
    }
}
