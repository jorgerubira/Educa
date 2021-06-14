using EducaClass.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace EducaClass.Services
{
    public class SQLService
    {
        public int EjecutarTransaccion(Contexto contexto, string script, Array parametros = null)
        {
            using (DbCommand comando = contexto.Database
                                        .GetDbConnection()
                                        .CreateCommand())
            {
                comando.CommandText = script;
                comando.CommandType = CommandType.Text;
                if (parametros != null)
                {
                    comando.Parameters.AddRange(parametros);
                }
                contexto.Database.OpenConnection();
                return comando.ExecuteNonQuery();
            }

        }

        public List<Tipo> EjecutarSQL<Tipo>(Contexto contexto,
                                    string sql,
                                    Func<DbDataReader, Tipo> leerRegistro,
                                    Array parametros = null)
        {
            using (DbCommand comando = contexto.Database
                                        .GetDbConnection()
                                        .CreateCommand())
            {
                comando.CommandText = sql;
                comando.CommandType = CommandType.Text;
                if (parametros != null)
                {
                    comando.Parameters.AddRange(parametros);
                }
                contexto.Database.OpenConnection();
                using (DbDataReader dr = comando.ExecuteReader())
                {
                    List<Tipo> resultado = new List<Tipo>();
                    while (dr.Read())
                    {
                        resultado.Add(leerRegistro(dr));
                    }
                    return resultado;
                }
            }
        }
    }
}
