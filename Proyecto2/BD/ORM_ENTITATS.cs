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
    class ORM_ENTITATS
    {
        public static List<ENTITATS> SelectAllENTITATS(ref String mensaje)
        {
            List<ENTITATS> _entitats = null;

            try
            {
                _entitats = (from p in ORM.bd.ENTITATS
                             select p).ToList();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                mensaje = BD.ORM.MensajeError(sqlEx);
            }
            catch(EntityException ex)
            {
                SqlException sqlEx = (SqlException)ex.InnerException;
                mensaje = BD.ORM.MensajeError(sqlEx);
            }
            

            return _entitats;
        }

        public static List<ENTITATS> SelectEntitatByID(int id, ref String mensaje)
        {
            List<ENTITATS> _entitats = null;

            try
            {
                _entitats = (from p in ORM.bd.ENTITATS
                           where p.id == id
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


            return _entitats;
        }

        public static String InsertENTITAT(String nom, String temporada, String adreca, String NIF, String correu, String password)
        {
            ENTITATS entitat = new ENTITATS();

            entitat.nom = nom;
            entitat.temporada = temporada;
            entitat.adreca = adreca;
            entitat.NIF = NIF;
            entitat.correu = correu;
            entitat.password = password;

            ORM.bd.ENTITATS.Add(entitat);

            return ORM.SaveChanges();
        }

        public static String DeleteENTITATS(ENTITATS entitat)
        {
            ORM.bd.ENTITATS.Remove(entitat);

            return ORM.SaveChanges();
        }

        public static String UpdateENTITATS(int id, String nom, String temporada, String adreca, String NIF, String correu, String password)
        {
            ENTITATS entitat = ORM.bd.ENTITATS.Find(id);

            entitat.nom = nom;
            entitat.temporada = temporada;
            entitat.adreca = adreca;
            entitat.NIF = NIF;
            entitat.correu = correu;
            entitat.password = password;

            return ORM.SaveChanges();
        }
    }
}
