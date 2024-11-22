using Npgsql;
using System.ComponentModel;
using System.Data;

namespace Libros_Leidos.Models
{
    public class Usuarios : Conexion //La herencia se hace con 2 puntos 

    {
        public List<Usuarios> GetUsuarios()
        {
            const string sql = "SELECT * FROM usuarios;"; //Comando de SQL
            DataTable tabla = GetQuery(sql);
            List<Usuarios> LstUsuarios = new List<Usuarios>();
            if (tabla.Rows.Count < 1)
            {
                return LstUsuarios;
            }
            foreach (DataRow fila in tabla.Rows)
            {
                LstUsuarios.Add(new Usuarios((int)fila["id_usuario"],
                    (string)fila["nombre"],
                    (string)fila["email"],
                    (string)fila["contrasena"]));
            }//End foreach
            return LstUsuarios;
        }//End metodo


        public void AddUsuario(Usuarios uss)
        { //Ayuda a agregar registros a la BD
            const string sql = "Insert into usuarios (nombre, email, contrasena) values(:nom, :ema, :con);"; //Insertar en resgistro en la BD
            List<NpgsqlParameter> lstparam = new List<NpgsqlParameter>(); //Lista de parametros
            NpgsqlParameter paramNombre = new NpgsqlParameter(":nom", uss.nombre); //Objetos parametros,en donde cada uno vendra el identificador y el objeto parametro
            NpgsqlParameter paramEma = new NpgsqlParameter(":ema", uss.email);//Objetos parametros,en donde cada uno vendra el identificador y el objeto parametro
            NpgsqlParameter paramCon = new NpgsqlParameter(":con", uss.contrasena);//Objetos parametros,en donde cada uno vendra el identificador y el objeto parametro
            lstparam.Add(paramNombre); lstparam.Add(paramEma); lstparam.Add(paramCon);//Agregar los parametros
            GetQuery(sql, lstparam);//Mandar a llamar para que se encargue de la consulta
        }

        public void EditUsuario(Usuarios uss)
        {
            const string SQL = "Update usuarios set nombre=:nom, email=:ema, contrasena=:con where id_usuario=:id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", uss.id_usuario);
            NpgsqlParameter paramNom = new NpgsqlParameter(":nom", uss.nombre);
            NpgsqlParameter paramEma = new NpgsqlParameter(":ema", uss.email);
            NpgsqlParameter paramCon = new NpgsqlParameter(":con", uss.contrasena);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>() { paramId, paramNom, paramEma, paramCon };
            GetQuery(SQL, lstParams);

        }

        public void DeleteUsuario(Usuarios uss)
        {
            const string SQL = "DELETE FROM usuarios WHERE id_usuario=:id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", uss.id_usuario);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>() { paramId };
            GetQuery(SQL, lstParams);

        }
     
            public int id_usuario { get; set; }

            public string nombre { get; set; }

            public string email { get; set; }

            public string contrasena { get; set; }

            public Usuarios()
            {

            }

            public Usuarios(int id_usuario)
            {
                this.id_usuario = id_usuario;
            }

        public Usuarios(int id_usuario, string nombre, string email, string contrasena) : this(id_usuario)
            {
                
                this.nombre = nombre;
                this.email = email;
                this.contrasena = contrasena;

            }
            

        }
    }

