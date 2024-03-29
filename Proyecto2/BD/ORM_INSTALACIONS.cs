﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2.BD
{
    class ORM_INSTALACION
    {
        public static List<INSTALACIONS> SelectAllINSTALACION(ref String mensaje)
        {
            List<INSTALACIONS> _instalacions = null;

            try
            {
                _instalacions = (from p in ORM.bd.INSTALACIONS
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

            return _instalacions;
        }

        public static String InsertINSTALACION(String nom, bool gestioExterna, String adreca)
        {

            INSTALACIONS instalacion = new INSTALACIONS();

            instalacion.nom = nom;
            instalacion.gestioExterna = gestioExterna;
            instalacion.adreca = adreca;

            ORM.bd.INSTALACIONS.Add(instalacion);

            return ORM.SaveChanges();
        }

        public static String DeleteINSTALACION(INSTALACIONS instalacion)
        {
            ORM.bd.INSTALACIONS.Remove(instalacion);

            return ORM.SaveChanges();
        }

        public static String UpdateINSTALACION(int id, INSTALACIONS newInstalacion)
        {
            INSTALACIONS instalacion = ORM.bd.INSTALACIONS.Find(id);

            instalacion.nom = newInstalacion.nom;
            instalacion.gestioExterna = newInstalacion.gestioExterna;
            instalacion.adreca = newInstalacion.adreca;

            instalacion.HORARIS_INSTALACIONS = newInstalacion.HORARIS_INSTALACIONS;
            instalacion.ESPAIS = newInstalacion.ESPAIS;

            return ORM.SaveChanges();
        }
    }
}
