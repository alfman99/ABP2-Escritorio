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
    class ORM_CATEGORIES
    {
        public static List<CATEGORIES> SelectAllCATEGORIAS(ref String mensaje)
        {
            List<CATEGORIES> _categorias = null;

            try
            {
                _categorias = (from p in ORM.bd.CATEGORIES
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


            return _categorias;
        }

        public static String InsertCATEGORIA(String nom)
        {
            CATEGORIES categoria = new CATEGORIES();

            categoria.nom = nom;

            ORM.bd.CATEGORIES.Add(categoria);

            return ORM.SaveChanges();
        }

        public static void DeleteCATEGORIA(CATEGORIES categoria)
        {
            ORM.bd.CATEGORIES.Remove(categoria);

            ORM.SaveChanges();
        }

        public static String UpdateCATEGORIA(int id, String nom)
        {
            CATEGORIES categoria = ORM.bd.CATEGORIES.Find(id);

            categoria.nom = nom;

            return ORM.SaveChanges();
        }
    }
}
