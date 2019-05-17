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
    class ORM_USUARIS
    {
        public static List<USUARIS> SelectAllUSUARIS(ref String mensaje)
        {
            List<USUARIS> _usuaris = null;

            try
            {
                _usuaris = (from p in ORM.bd.USUARIS
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

            return _usuaris;
        }

        public static String InsertUSUARIS(String nom, String correu, String contrasenya)
        {

            USUARIS usuari = new USUARIS();

            usuari.nom = nom;
            usuari.correu = correu;
            usuari.contrasenya = contrasenya;

            ORM.bd.USUARIS.Add(usuari);

            return ORM.SaveChanges();
        }

        public static void DeleteUSUARIS(USUARIS usuari)
        {
            ORM.bd.USUARIS.Remove(usuari);

            ORM.SaveChanges();
        }

        public static String UpdateUSUARIS(int id, String nom, String correu, String contrasenya)
        {
            USUARIS usuari = ORM.bd.USUARIS.Find(id);

            usuari.nom = nom;
            usuari.correu = correu;
            usuari.contrasenya = contrasenya;

            return ORM.SaveChanges();
        }
    }
}
