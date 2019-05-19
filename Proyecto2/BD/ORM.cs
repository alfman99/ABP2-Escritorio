using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto2.BD
{
    public static class ORM
    {
        public static entiespaisEntities1 bd = new entiespaisEntities1();

        public static String MensajeError(SqlException sqlEx)
        {
            String mensaje;
            switch (sqlEx.Number)
            {
                case -1:
                    mensaje = "Error de conexion con el servidor";
                    break;
                case 547:
                    mensaje = "Tiene datos relacionados";
                    break;
                case 2601:
                    mensaje = "Datos duplicados";
                    break;
                case 2627:
                    mensaje = "Datos duplicados";
                    break;
                case 4060:
                    mensaje = "No se encuentra la base de datos";
                    break;
                case 18456:
                    mensaje = "Usuario o contraseña incorrectos (Base de datos)";
                    break;
                default:
                    mensaje = sqlEx.Number + " " + sqlEx.Message;
                    break;
            }

            return mensaje;
        }

        public static void RejectChanges()
        {
            foreach (DbEntityEntry entry in bd.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public static String SaveChanges()
        {
            String mensaje = "";

            try
            {
                bd.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ORM.RejectChanges();
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                mensaje = MensajeError(sqlEx);
            }

            return mensaje;
        }
    }
}
