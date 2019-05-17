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
    class ORM_SEXES
    {
        public static List<SEXES> SelectAllSEXES(ref String mensaje)
        {
            List<SEXES> _sexes = null;

            try
            {
                _sexes = (from p in ORM.bd.SEXES
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

            return _sexes;
        }

        public static String InsertSEXES(String nom)
        {
            SEXES sexe = new SEXES();

            sexe.nom = nom;

            ORM.bd.SEXES.Add(sexe);

            return ORM.SaveChanges();
        }

        public static void DeleteSEXES(SEXES sexe)
        {
            ORM.bd.SEXES.Remove(sexe);

            ORM.SaveChanges();
        }

        public static String UpdateSEXES(int id, String nom)
        {
            SEXES sexe = ORM.bd.SEXES.Find(id);

            sexe.nom = nom;

            return ORM.SaveChanges();
        }
    }
}
