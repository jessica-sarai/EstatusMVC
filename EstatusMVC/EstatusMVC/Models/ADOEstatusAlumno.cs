using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EstatusMVC.Models
{
    public class ADOEstatusAlumno
    {
        private string _conexion;
        public ADOEstatusAlumno()
        {
            _conexion = ConfigurationManager.ConnectionStrings["EstatusAlumno"].ConnectionString;
        }

        public List<EstatusAlumno> consultarLista()
        {
            List<EstatusAlumno> lstEstatus = new List<EstatusAlumno>();
            string query = "select id, clave, nombre from EstatusAlumnos";

            try
            {
                using (SqlConnection con = new SqlConnection(_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        EstatusAlumno estatusAlum = new EstatusAlumno();
                        estatusAlum.id = Convert.ToInt32(rd["id"]);
                        estatusAlum.clave = Convert.ToString(rd["clave"]);
                        estatusAlum.nombre = Convert.ToString(rd["nombre"]);
                        lstEstatus.Add(estatusAlum);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar", ex);
            }
            return lstEstatus;
        }

        public List<EstatusAlumno> consultarListaUno(int id)
        {
            List<EstatusAlumno> lstEstatus = new List<EstatusAlumno>();
            try
            {
                using (SqlConnection con = new SqlConnection(_conexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_consultarEstatusUno", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        EstatusAlumno estatusAlumnos = new EstatusAlumno();
                        estatusAlumnos.id = Convert.ToInt32(rd["id"]);
                        estatusAlumnos.clave = Convert.ToString(rd["clave"]);
                        estatusAlumnos.nombre = Convert.ToString(rd["nombre"]);
                        lstEstatus.Add(estatusAlumnos);
                    }
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar un dato", ex);
            }
            return lstEstatus;

        }


        public void agregarLista(string clave, string nombre)
        {
            List<EstatusAlumno> lstAgregar = new List<EstatusAlumno>();
            try
            {
                using (SqlConnection con = new SqlConnection(_conexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_AgregarEstatus", con);
                    EstatusAlumno estatusAlumnos = new EstatusAlumno();

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar un nuevo Estatus", ex);
            }
        }

        public void actualizarEstatus(int id, string clave, string nombre)
        {
            List<EstatusAlumno> _lista = consultarLista();

            try
            {
                using (SqlConnection con = new SqlConnection(_conexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_ActualizarEstatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar", ex);
            }
        }

        public void eliminarEstatus(int id)
        {
            List<EstatusAlumno> lstEstatus = new List<EstatusAlumno>();
            try
            {
                using (SqlConnection con = new SqlConnection(_conexion))
                {
                    SqlCommand cmd = new SqlCommand("delete from EstatusAlumnos where id=" + id, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar registro", ex);
            }
        }
    }
}