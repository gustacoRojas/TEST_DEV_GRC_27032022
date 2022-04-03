using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIExamenTokaRest.Models;
using static APIExamenTokaRest.Models.ConfigModel;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace APIExamenTokaRest.Data
{
    public class ConexionBD : IConexionBD
    {
        
        private SqlConnection _SqlConnetion;

        public ConexionBD() {
            _SqlConnetion = new SqlConnection(APIConfigModel.cadenaConexion);
        }

        public IList<persona> ObtenerListaPersonasBD()
        {
            IList<persona> arregloListaPersonas = new List<persona>();

            var sqlInsersion = "SELECT IdPersonaFisica,Nombre,ApellidoPaterno,ApellidoMaterno,RFC,FechaNacimiento,UsuarioAgrega FROM dbo.Tb_PersonasFisicas WHERE Activo = 1";
       

            SqlCommand cmdIns = null;
            try
            {
                cmdIns = new SqlCommand(sqlInsersion, _SqlConnetion);

                _SqlConnetion.Open();

                var res = cmdIns.ExecuteReader();

                while (res.Read())
                {
                    arregloListaPersonas.Add(new persona()
                    {
                        id = res.GetInt32(0),
                        nombre = res.GetString(1),
                        apellidoPa = res.GetString(2),
                        apellidoMa = res.GetString(3),
                        rfc = res.GetString(4),
                        fechaNacimiento = res.GetDateTime(5),
                        usuarioAgrega = res.GetInt32(6)
                    });
                }

                _SqlConnetion.Close();
                cmdIns.Dispose();
                
            }
            catch (Exception Excepcion) {
                
                EventLog.WriteEntry("APIExamenTokaRest", Excepcion.Message, EventLogEntryType.Information, 999);
            }
            finally {
                _SqlConnetion.Close();
            
            };

            return arregloListaPersonas;
        }

        public string AgregaPersonaBD(persona personaFiscal)
        {
            var sqlInsersion =
                "EXEC dbo.sp_AgregarPersonaFisica @Nombre, @ApellidoPaterno, @ApellidoMaterno, @RFC, @FechaNacimiento, @UsuarioAgrega;";

            string msjResult= string.Empty;
            SqlCommand cmdIns = null;
            try
            {

                cmdIns = new SqlCommand(sqlInsersion, _SqlConnetion);

                _SqlConnetion.Open();


                cmdIns.Parameters.AddWithValue("@Nombre", personaFiscal.nombre);
                cmdIns.Parameters.AddWithValue("@ApellidoPaterno", personaFiscal.apellidoPa);
                cmdIns.Parameters.AddWithValue("@ApellidoMaterno", personaFiscal.apellidoMa);
                cmdIns.Parameters.AddWithValue("@RFC", personaFiscal.rfc);
                cmdIns.Parameters.AddWithValue("@FechaNacimiento", personaFiscal.fechaNacimiento);
                cmdIns.Parameters.AddWithValue("@UsuarioAgrega", personaFiscal.usuarioAgrega);

                var res = cmdIns.ExecuteReader();

                while (res.Read())
                {
                    msjResult = res.GetString(1);
                }

                _SqlConnetion.Close();
                cmdIns.Dispose();

            }
            catch (Exception Excepcion)
            {
                msjResult = Excepcion.Message;
            }
            finally
            {
                _SqlConnetion.Close();

            };

            return msjResult;
        }

        public string ModificaPersonaBD(persona personaFiscal)
        {
            var sqlInsersion =
                "EXEC dbo.sp_ActualizarPersonaFisica @IdPersonaFisica, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @RFC, @FechaNacimiento, @UsuarioAgrega;";

            string msjResult = string.Empty;
            SqlCommand cmdIns = null;
            try
            {
                
                cmdIns = new SqlCommand(sqlInsersion, _SqlConnetion);

                _SqlConnetion.Open();


                cmdIns.Parameters.AddWithValue("@IdPersonaFisica", personaFiscal.id);
                cmdIns.Parameters.AddWithValue("@Nombre", personaFiscal.nombre);
                cmdIns.Parameters.AddWithValue("@ApellidoPaterno", personaFiscal.apellidoPa);
                cmdIns.Parameters.AddWithValue("@ApellidoMaterno", personaFiscal.apellidoMa);
                cmdIns.Parameters.AddWithValue("@RFC", personaFiscal.rfc);
                cmdIns.Parameters.AddWithValue("@FechaNacimiento", personaFiscal.fechaNacimiento);
                cmdIns.Parameters.AddWithValue("@UsuarioAgrega", personaFiscal.usuarioAgrega);


                var res = cmdIns.ExecuteReader();

                while (res.Read())
                {
                    msjResult = res.GetString(1);
                }

                _SqlConnetion.Close();
                cmdIns.Dispose();

            }
            catch (Exception Excepcion)
            {
                msjResult = Excepcion.Message;
            }
            finally
            {
                _SqlConnetion.Close();

            };

            return msjResult;
        }

        public string EliminaPersonaBD(int idpersona)
        {    
            string msjResult = string.Empty;
            SqlCommand cmdIns = null;
            try
            {

                var sqlInsersion = "EXEC dbo.sp_EliminarPersonaFisica @IdPersonaFisica";

                cmdIns = new SqlCommand(sqlInsersion, _SqlConnetion);

                _SqlConnetion.Open();

                cmdIns.Parameters.AddWithValue("@IdPersonaFisica", idpersona);

                using (SqlDataReader reader = cmdIns.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        msjResult = reader.GetString(1);
                    }
                    
                }

                if (msjResult.Equals(string.Empty) || msjResult == null) { 
                    msjResult = APIConfigModel.resultadoOk;
                }

                _SqlConnetion.Close();
                cmdIns.Dispose();

            }
            catch (Exception Excepcion)
            {
                msjResult = Excepcion.Message;
            }
            finally
            {
                _SqlConnetion.Close();

            };

            return msjResult;
        }

    }
}
