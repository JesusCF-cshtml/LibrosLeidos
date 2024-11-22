using Libros_Leidos.Models;
using Npgsql;
using System.ComponentModel;
using System.Data;

namespace Libros_Leidos.Models
{
    public class Libros : Conexion // Herencia de la clase Conexion
    {
        public int id_libro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public int id_genero { get; set; }
        public DateTime fecha_publicacion { get; set; }

        public Libros() { }

        public Libros(int id_libro)
        {
            this.id_libro = id_libro;
        }

        public Libros(int id_libro, string titulo, string autor, int id_genero, DateTime fecha_publicacion) : this(id_libro)
        {
            this.titulo = titulo;
            this.autor = autor;
            this.id_genero = id_genero;
            this.fecha_publicacion = fecha_publicacion;
        }

        public List<Libros> GetLibros()
        {
            const string sql = "SELECT * FROM libros;"; // Comando de SQL
            DataTable tabla = GetQuery(sql);
            List<Libros> LstLibros = new List<Libros>();
            if (tabla.Rows.Count < 1)
            {
                return LstLibros;
            }
            foreach (DataRow fila in tabla.Rows)
            {
                LstLibros.Add(new Libros(
                    (int)fila["id_libro"],
                    (string)fila["titulo"],
                    (string)fila["autor"],
                    (int)fila["id_genero"],
                    (DateTime)fila["fecha_publicacion"]));
            } // End foreach
            return LstLibros;
        } // End metodo

public void AddLibro(Libros libro)
{
    // Ayuda a agregar registros a la BD
    const string sql = "INSERT INTO libros (titulo, autor, id_genero, fecha_publicacion) VALUES (:tit, :aut, :gen, :fec);"; // Insertar en registro en la BD

    List<NpgsqlParameter> lstparam = new List<NpgsqlParameter>(); // Lista de parámetros

    // Crear objetos de parámetros
    NpgsqlParameter paramTitulo = new NpgsqlParameter(":tit", libro.titulo); // Parámetro para el título
    NpgsqlParameter paramAutor = new NpgsqlParameter(":aut", libro.autor); // Parámetro para el autor
    NpgsqlParameter paramGenero = new NpgsqlParameter(":gen", libro.id_genero); // Parámetro para el id_genero
    NpgsqlParameter paramFecha = new NpgsqlParameter(":fec", libro.fecha_publicacion); // Parámetro para la fecha de publicación

    // Agregar los parámetros a la lista
    lstparam.Add(paramTitulo);
    lstparam.Add(paramAutor);
    lstparam.Add(paramGenero);
    lstparam.Add(paramFecha);

    // Mandar a llamar para que se encargue de la consulta
    GetQuery(sql, lstparam);
}


        public void EditLibro(Libros libro)
        {
            const string sql = "UPDATE libros SET titulo = :tit, autor = :aut, id_genero = :gen, fecha_publicacion = :fec WHERE id_libro = :id;";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter(":id", libro.id_libro),
                new NpgsqlParameter(":tit", libro.titulo),
                new NpgsqlParameter(":aut", libro.autor),
                new NpgsqlParameter(":gen", libro.id_genero),
                new NpgsqlParameter(":fec", libro.fecha_publicacion)
            };
            GetQuery(sql, lstParams);
        }

        public void DeleteLibro(Libros libro)
        {
            const string sql = "DELETE FROM libros WHERE id_libro = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", libro.id_libro);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>() { paramId };
            GetQuery(sql, lstParams);
        }
    }
}