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
    class ORM_TELEFONS
    {
        public static List<TELEFONS> SelectAllTELEFONS(ref String mensaje)
        {
            List<TELEFONS> _telefons = null;

            try
            {
                _telefons = (from p in ORM.bd.TELEFONS
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

            return _telefons;
        }


        public static String InsertTELEFON(String rao, String numero, int id_entitat)
        {

            TELEFONS telefon = new TELEFONS();

            telefon.rao = rao;
            telefon.numero = numero;
            telefon.id_entitat = id_entitat;

            ORM.bd.TELEFONS.Add(telefon);

            return ORM.SaveChanges();
        }

        public static void DeleteTELEFON(TELEFONS telefon)
        {
            ORM.bd.TELEFONS.Remove(telefon);

            ORM.SaveChanges();
        }

        public static String UpdateTELEFON(int id, String rao, String numero, int id_entitat)
        {
            TELEFONS telefon = ORM.bd.TELEFONS.Find(id);

            telefon.rao = rao;
            telefon.numero = numero;
            telefon.id_entitat = id_entitat;

            return ORM.SaveChanges();
        }
    }
}
