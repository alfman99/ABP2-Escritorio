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
    class ORM_TIPUS_ACTIVITAT
    {
        public static List<TIPUS_ACTIVITAT> SelectAllTIPUS_ACTIVITAT(ref String mensaje)
        {
            List<TIPUS_ACTIVITAT> _tipus_activitats = null;

            try
            {
                _tipus_activitats = (from p in ORM.bd.TIPUS_ACTIVITAT
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

            return _tipus_activitats;
        }

        public static String InsertTIPUS_ACTIVITAT(String nom)
        {

            TIPUS_ACTIVITAT tipus_activitat = new TIPUS_ACTIVITAT();

            tipus_activitat.nom = nom;

            ORM.bd.TIPUS_ACTIVITAT.Add(tipus_activitat);

            return ORM.SaveChanges();
        }

        public static void DeleteTIPUS_ACTIVITAT(TIPUS_ACTIVITAT tipus_activitat)
        {
            ORM.bd.TIPUS_ACTIVITAT.Remove(tipus_activitat);

            ORM.SaveChanges();
        }

        public static String UpdateTIPUS_ACTIVITAT(int id, String nom)
        {
            TIPUS_ACTIVITAT tipus_activitat = ORM.bd.TIPUS_ACTIVITAT.Find(id);

            tipus_activitat.nom = nom;

            return ORM.SaveChanges();
        }
    }
}
