using NLogProject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FileReader : IFileReader
    {
        public FileReader()
        {
            Logger = new LogMaker();
            GetFilms();
        }
        FilmModel FM = new FilmModel();
        public List<FilmModel> Result { get; set; } = new List<FilmModel>();
        ILogMaker Logger;
        //NLogProject.LogMaker Logger = new NLogProject.LogMaker();
        public List<FilmModel> GetFilms() //получает пленки из бд
        {
                //using (SqlConnection cn = new SqlConnection("Server = IVAN-PC\\SQLEXPRESS; Database = FilmDataBase; Trusted_Connection = True;")) //работает, но строка подключения не в конфиге. На всякий случай оставлю
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)) //Читает строку из конфига
                {
                    cn.Open();
                    string sql = string.Format("SELECT Firm, Name, Type, ISO, Cost FROM dbo.Films");
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    var FilmReader = cmd.ExecuteReader();
                    while (FilmReader.Read())
                    {
                        FilmModel f = new FilmModel();
                        f.Firm = (string)FilmReader["Firm"];
                        f.Name = (string)FilmReader["Name"];
                        f.Type = (string)FilmReader["Type"];
                        f.ISO = (int)FilmReader["ISO"];
                        f.Cost = (int)FilmReader["Cost"];
                        Result.Add(f);
                    }
                }
            Logger.LogInfo("Данные получены из бд");
            return Result;

        }

    }
}
