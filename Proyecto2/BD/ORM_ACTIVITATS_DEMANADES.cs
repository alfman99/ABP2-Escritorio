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
    class ORM_ACTIVITATS_DEMANADES
    {
        public static List<ACTIVITATS_DEMANADES> SelectAllACTIVITATS(ref String mensaje)
        {
            List<ACTIVITATS_DEMANADES> _activitats_demanades = null;

            try
            {
                _activitats_demanades = (from p in ORM.bd.ACTIVITATS_DEMANADES
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


            return _activitats_demanades;
        }

        public static String InsertACTIVITATS_DEMANADES(String nom, System.TimeSpan durada, int id_equip, int id_espai, int id_tipus_activitat, int num_dies, bool assignada)
        {
            ACTIVITATS_DEMANADES activitat_demanada = new ACTIVITATS_DEMANADES();
            activitat_demanada.nom = nom;
            activitat_demanada.durada = durada;
            activitat_demanada.id_equip = id_equip;
            activitat_demanada.id_espai = id_espai;
            activitat_demanada.id_tipus_activitat = id_tipus_activitat;
            activitat_demanada.num_dies = num_dies;
            activitat_demanada.assignada = assignada;

            ORM.bd.ACTIVITATS_DEMANADES.Add(activitat_demanada);

            return ORM.SaveChanges();
        }

        public static String DeleteACTIVITAT(ACTIVITATS_DEMANADES activitat)
        {
            ORM.bd.ACTIVITATS_DEMANADES.Remove(activitat);

            return ORM.SaveChanges();
        }

        public static String UpdateACTIVITATS_DEMANADES(int id, String nom, System.TimeSpan durada, int id_equip, int id_espai, int id_tipus_activitat, int num_dies, bool assignada)
        {
            ACTIVITATS_DEMANADES activitat_demanada = ORM.bd.ACTIVITATS_DEMANADES.Find(id);

            activitat_demanada.nom = nom;
            activitat_demanada.durada = durada;
            activitat_demanada.id_equip = id_equip;
            activitat_demanada.id_espai = id_espai;
            activitat_demanada.id_tipus_activitat = id_tipus_activitat;
            activitat_demanada.num_dies = num_dies;
            activitat_demanada.assignada = assignada;

            return ORM.SaveChanges();
        }
    }
}
