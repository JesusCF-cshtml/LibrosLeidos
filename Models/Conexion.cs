using Npgsql; //Se importa
using System.Configuration;
using System.Data;

namespace Libros_Leidos.Models
{
    public class Conexion
    {
        private string strConexion;
        protected Conexion()//Cadena de la conexion
        {
            strConexion = "Server=localhost;Username=postgres;Database=libros;Password=root;";
            //URL de server con localhost (el mismo equipo)
            //No es bueno jugar con el super usurio, es mejor usar uno con menor rango
            //Colocar el nombre de nuestra base de datos

        }
        protected DataTable GetQuery(string sql)//Se reroena una bonita tabla de datos
        {
            DataTable tabla = new();//Se crea un objeto

            NpgsqlDataAdapter adaptador = new NpgsqlDataAdapter();//new(); Objeto adaptador, permite transformar datos
            try //Atrapa las excepciones (Error en la ejecución del codigo
            {
                using NpgsqlConnection _con = new NpgsqlConnection();//new(); Objeto temporal(Creado por el using)
                _con.ConnectionString = strConexion; //Abrir la conexion
                _con.Open();
                using NpgsqlCommand comando = new NpgsqlCommand(); //new(); Objeto temporal comando
                comando.Connection = _con; //Se indica que conexion utilizara
                comando.CommandText = sql; //Se pasa la consulta con el sql
                adaptador.SelectCommand = comando; //Transforma datos
                using NpgsqlDataReader lector = comando.ExecuteReader(); //obejeto temporal (lector)
                if (lector.HasRows) //Si ese lector tiene filas?
                {
                    tabla.Load(lector); //Tiene registros, tiene datos

                }
                lector.Close(); //Cierre de lector
                _con.Close(); //Cierre de _con
            }
            catch (Exception e) //Atrapa de exepcion
            {
                System.Console.WriteLine(e.Message); //Se escribe la exepcion
            }
            return tabla; //Regresa mi tabla con datos o vacia
        }//end método

        protected DataTable GetQuery(string sql, List<NpgsqlParameter> parametros)
        { //Va a recibir una lista de parametros para evitar inyeccion SQL
            DataTable tabla = new DataTable(); //Jugar con un obeto tipo data table
            NpgsqlDataAdapter adaptador = new NpgsqlDataAdapter();//Es ocupado para llenar la tabla
            using (NpgsqlConnection _con = new NpgsqlConnection())
            { //Crear mi objeto conexion, es el puente entre aplicacion y BD
              //Using crea objeto de manera temporal
                _con.ConnectionString = strConexion;//Se pasa la cadena de conexion para indicar donde esta la (BD, etc...)
                _con.Open();//Abrir el puente
                using (NpgsqlCommand comando = new NpgsqlCommand())
                {//Objeto que manda al puente la instruccion SQL que se generara
                    comando.Connection = _con;//Indicar que puente va a utilizar
                    comando.CommandText = sql;//Cual va a ser la sentencia a ejecutar
                    comando.Parameters.Clear();//Evitar situaciones con los parametros (basura, etc...)
                    foreach (NpgsqlParameter param in parametros)
                    {//Lista de parametros segun la instruccion
                     //Param sera variable auxiliar y se pasara la lista a PARAMETROS
                        comando.Parameters.Add(param);//Metodo ADD, se agrega a la lista un parametro por cada parametro
                    }
                    adaptador.SelectCommand = comando;//LE paso el comando seleccionado y le paso el comando que ya tiene la consulta,los parametros,etc...
                    adaptador.Fill(tabla);//Lleno mmi objeto tabla
                }
                _con.Close();//Cerrar la conexion
            }
            return tabla;//Regreso mi tabla
        }
    }
}
