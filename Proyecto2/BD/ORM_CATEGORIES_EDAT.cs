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
    class ORM_CATEGORIES_EDAT
    {
        public static List<CATEGORIES_EDAT> SelectAllCATEGORIES_EDAT(ref String mensaje)
        {
            List<CATEGORIES_EDAT> _categories_edat = null;

            try
            {
                _categories_edat = (from p in ORM.bd.CATEGORIES_EDAT
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


            return _categories_edat;
        }

        public static String InsertCATEGORIES_EDAT(String nom)
        {
            CATEGORIES_EDAT categoria_edat = new CATEGORIES_EDAT();

            categoria_edat.nom = nom;

            ORM.bd.CATEGORIES_EDAT.Add(categoria_edat);

            return ORM.SaveChanges();
        }

        public static void DeleteCATEGORIES_EDAT(CATEGORIES_EDAT categoria_edat)
        {
            ORM.bd.CATEGORIES_EDAT.Remove(categoria_edat);

            ORM.SaveChanges();
        }

        public static String UpdateCATEGORIES_EDAT(int id, String nom)
        {
            CATEGORIES_EDAT categoria_edat = ORM.bd.CATEGORIES_EDAT.Find(id);

            categoria_edat.nom = nom;

            return ORM.SaveChanges();
        }
    }
}
