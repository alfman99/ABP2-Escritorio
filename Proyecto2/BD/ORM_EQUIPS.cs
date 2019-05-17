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
    class ORM_EQUIPS
    {
        public static List<EQUIPS> SelectAllEQUIPS(ref String mensaje)
        {
            List<EQUIPS> _equips = null;

            try
            {
                _equips = (from p in ORM.bd.EQUIPS
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


            return _equips;
        }

        public static String InsertEQUIP(String nom, int id_entitat, int id_esport, int id_sexe, int id_categoria, int id_categoria_edat, int id_competicio)
        {
            EQUIPS equip = new EQUIPS();

            equip.nom = nom;
            equip.id_entitat = id_entitat;
            equip.id_esport = id_esport;
            equip.id_sexe = id_sexe;
            equip.id_categoria = id_categoria;
            equip.id_categoria_edat = id_categoria_edat;
            equip.id_competicio = id_competicio;

            ORM.bd.EQUIPS.Add(equip);

            return ORM.SaveChanges();
        }

        public static String DeleteEQUIP(EQUIPS equip)
        {
            ORM.bd.EQUIPS.Remove(equip);

            return ORM.SaveChanges();
        }

        public static String UpdateEQUIP(int id, String nom, int id_entitat, int id_esport, int id_sexe, int id_categoria, int id_categoria_edat, int id_competicio)
        {
            EQUIPS equip = ORM.bd.EQUIPS.Find(id);

            equip.nom = nom;
            equip.id_entitat = id_entitat;
            equip.id_esport = id_esport;
            equip.id_sexe = id_sexe;
            equip.id_categoria = id_categoria;
            equip.id_categoria_edat = id_categoria_edat;
            equip.id_competicio = id_competicio;

            return ORM.SaveChanges();
        }
    }
}
