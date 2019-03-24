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
    class ORM_ESPAIS
    {
        public static List<ESPAIS> SelectAllESPAIS(ref String mensaje)
        {
            List<ESPAIS> _espais = null;

            try
            {
                _espais = (from p in ORM.bd.ESPAIS
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

            return _espais;
        }

        public static String InsertESPAI(String nom, double preu, bool exterior, int id_instalacio)
        {
            ESPAIS espai = new ESPAIS();

            espai.nom = nom;
            espai.preu = preu;
            espai.exterior = exterior;
            espai.id_instalacio = id_instalacio;

            ORM.bd.ESPAIS.Add(espai);

            return ORM.SaveChanges();
        }

        public static void DeleteESPAI(ESPAIS espai)
        {
            ORM.bd.ESPAIS.Remove(espai);

            ORM.SaveChanges();
        }

        public static String UpdateESPAI(int id, String nom, double preu, bool exterior, int id_instalacio)
        {
            ESPAIS espai = ORM.bd.ESPAIS.Find(id);

            espai.nom = nom;
            espai.preu = preu;
            espai.exterior = exterior;
            espai.id_instalacio = id_instalacio;

            return ORM.SaveChanges();
        }
    }
}
