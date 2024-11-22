using Npgsql;
using System.ComponentModel;
using System.Data;

namespace Libros_Leidos.Models
{
    public class Generos : Conexion // Hereda de la clase Conexion
    {
        public int id_genero { get; set; }
        public string nombre { get; set; }

        public Generos() { }

        public Generos(int id_genero)
        {
            this.id_genero = id_genero;
        }

        public Generos(int id_genero, string nombre) : this(id_genero)
        {
            this.nombre = nombre;
        }

        public List<Generos> GetGeneros()
        {
            const string sql = "SELECT * FROM generos;";
            DataTable tabla = GetQuery(sql);
            List<Generos> LstGeneros = new List<Generos>();

            if (tabla.Rows.Count < 1)
            {
                return LstGeneros;
            }

            foreach (DataRow fila in tabla.Rows)
            {
                LstGeneros.Add(new Generos(
                    (int)fila["id_genero"],
                    (string)fila["nombre"]));
            }
            return LstGeneros;
        }

        public Generos GetGenerosById(int id)
        {
            const string sql = "SELECT * FROM generos WHERE id_genero = :id;";
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", id)
            };
            DataTable tabla = GetQuery(sql, parameters);

            if (tabla.Rows.Count == 0)
            {
                return null; // Si no se encuentra el género, devolver null
            }

            DataRow fila = tabla.Rows[0];
            return new Generos(
                (int)fila["id_genero"],
                (string)fila["nombre"]
            );
        }

        public void AddGenero(Generos genero)
        {
            const string sql = "INSERT INTO generos (nombre) VALUES (:nom);";
            List<NpgsqlParameter> lstparam = new List<NpgsqlParameter>();
            NpgsqlParameter paramNombre = new NpgsqlParameter(":nom", genero.nombre);
            lstparam.Add(paramNombre);
            GetQuery(sql, lstparam);
        }

        public void EditGenero(Generos genero)
        {
            const string sql = "UPDATE generos SET nombre = :nom WHERE id_genero = :id;";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", genero.id_genero),
                new NpgsqlParameter(":nom", genero.nombre)
            };
            GetQuery(sql, lstParams);
        }

        public void DeleteGenero(Generos genero)
        {
            const string sql = "DELETE FROM generos WHERE id_genero = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", genero.id_genero);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(sql, lstParams);
        }
    }
}
