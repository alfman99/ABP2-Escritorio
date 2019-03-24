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
    public static class ORM_ACTIVITATS
    {
        public static List<ACTIVITATS> SelectAllACTIVITATS(ref String mensaje)
        {
            List<ACTIVITATS> _activitats = null;

            try
            {
                _activitats = (from p in ORM.bd.ACTIVITATS
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


            return _activitats;
        }

        public static String InsertACTIVITAT(String nom, int id_equip, int id_tipus_activitat, int id_activitat_demanada, int id_usuari, DateTime data_ultima_mod, int id_espai)
        {
            ACTIVITATS activitat = new ACTIVITATS();
            activitat.nom = nom;
            activitat.id_equip = id_equip;
            activitat.id_tipus_activitat = id_tipus_activitat;
            activitat.id_activitat_demanada = id_activitat_demanada;
            activitat.id_usuari = id_usuari;
            activitat.data_ultima_mod = data_ultima_mod;
            activitat.id_espai = id_espai;

            ORM.bd.ACTIVITATS.Add(activitat);

            return ORM.SaveChanges();
        }

        public static String DeleteACTIVITAT(ACTIVITATS activitat)
        {
            ORM.bd.ACTIVITATS.Remove(activitat);

            return ORM.SaveChanges();
        }

        public static String UpdateACTIVITAT(int id, String nom, int id_equip, int id_tipus_activitat, int id_activitat_demanada, int id_usuari, DateTime data_ultima_mod, int id_espai)
        {
            ACTIVITATS activitat = ORM.bd.ACTIVITATS.Find(id);
            activitat.nom = nom;
            activitat.id_equip = id_equip;
            activitat.id_tipus_activitat = id_tipus_activitat;
            activitat.id_activitat_demanada = id_activitat_demanada;
            activitat.id_usuari = id_usuari;
            activitat.data_ultima_mod = data_ultima_mod;
            activitat.id_espai = id_espai;

            return ORM.SaveChanges();
        }
    }
}
