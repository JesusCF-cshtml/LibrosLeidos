using System;
using System.Collections.Generic;
using System.Data;
using Npgsql; // Asegúrate de tener la referencia a Npgsql

namespace Libros_Leidos.Models
{
    public class Registros : Conexion // Herencia de la clase Conexion
    {
        internal int id_usuario;
        internal int id_libro;

        public int IdUsuario { get; set; } // Clave foránea a Usuarios
        public int IdLibro { get; set; } // Clave foránea a Libros
        public int Calificacion { get; set; } // Calificación del libro
        public string NotasPersonales { get; set; } // Notas del usuario sobre el libro
        public DateTime FechaLectura { get; set; } // Fecha en que se leyó el libro

        // Constructor vacío
        public Registros() { }

        // Constructor que inicializa todas las propiedades
        public Registros(int id_usuario, int id_libro, int calificacion, string notas_personales, DateTime fecha_lectura)
        {
            this.IdUsuario = id_usuario;
            this.IdLibro = id_libro;
            this.Calificacion = calificacion;
            this.NotasPersonales = notas_personales;
            this.FechaLectura = fecha_lectura;
        }

        // Método para obtener todos los registros
        public List<Registros> GetRegistros()
        {
            const string sql = "SELECT * FROM registros;"; // Comando de SQL
            DataTable tabla = GetQuery(sql); // Asume que GetQuery es un método en la clase base
            List<Registros> lstRegistros = new List<Registros>();

            if (tabla.Rows.Count < 1)
            {
                return lstRegistros;
            }

            foreach (DataRow fila in tabla.Rows)
            {
                lstRegistros.Add(new Registros(
                    (int)fila["id_usuario"],
                    (int)fila["id_libro"],
                    (int)fila["calificacion"],
                    (string)fila["nota_personal"],
                    (DateTime)fila["fecha_lectura"]));
            }
            return lstRegistros;
        }

        // Método para agregar un nuevo registro
        public void AddRegistro(Registros registro)
        {
            const string sql = "INSERT INTO registros (id_usuario, id_libro, calificacion, nota_personal, fecha_lectura) VALUES (:usr, :lib, :cal, :not, :fec);";

            List<NpgsqlParameter> lstParam = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":usr", registro.IdUsuario),
                new NpgsqlParameter(":lib", registro.IdLibro),
                new NpgsqlParameter(":cal", registro.Calificacion),
                new NpgsqlParameter(":not", registro.NotasPersonales),
                new NpgsqlParameter(":fec", registro.FechaLectura)
            };

            GetQuery(sql, lstParam); // Asume que GetQuery maneja la ejecución de la consulta
        }

        // Método para editar un registro existente
        public void EditRegistro(Registros registro)
        {
            const string sql = "UPDATE registros SET calificacion = :cal, nota_personal = :not, fecha_lectura = :fec WHERE id_usuario = :usr AND id_libro = :lib;";

            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":usr", registro.IdUsuario),
                new NpgsqlParameter(":lib", registro.IdLibro),
                new NpgsqlParameter(":cal", registro.Calificacion),
                new NpgsqlParameter(":not", registro.NotasPersonales),
                new NpgsqlParameter(":fec", registro.FechaLectura)
            };

            GetQuery(sql, lstParams);
        }

        // Método para eliminar un registro
        public void DeleteRegistro(Registros registro)
        {
            const string sql = "DELETE FROM registros WHERE id_usuario = :usr AND id_libro = :lib;";

            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":usr", registro.IdUsuario),
                new NpgsqlParameter(":lib", registro.IdLibro)
            };

            GetQuery(sql, lstParams);
        }
    }
}
