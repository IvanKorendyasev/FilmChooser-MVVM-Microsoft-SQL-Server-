using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;

namespace BL
{
    public class FileProcessing
    {
        DAL.FileReader FR = new DAL.FileReader();
        public List<Film> FilmsFromDB = new List<Film>();
        public float Temperature { get; set; }
        public string City { get; set; }
        NLogProject.LogMaker Logger = new NLogProject.LogMaker();
        public FileProcessing()
        {
            FilmsFromDB = GetFilmsFromDB();
            GetTempFromAPI();
        }

        public void GoToSite() //открывает сайт магазина
        {
            Process.Start("https://shop.sreda.photo/");
            Logger.LogInfo("Открыл сайт");
        }

        public List<Film> GetFilmsFromDB() //получает пленки из DAL
        {
            FilmsFromDB = new List<Film>();
            
            foreach (var item in FR.Result)
            {
                FilmsFromDB.Add(new Film() { Firm = item.Firm, Type = item.Type, Cost = item.Cost, ISO = item.ISO, Name = item.Name });
            }
            return FilmsFromDB;
        }

        public void GetTempFromAPI() //получает температуру на улице через API Open Weather Map
        {
            try
            {
                string url = ConfigurationManager.AppSettings["url"]; //url можно поменять в конфиге
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                Temperature = weatherResponse.Main.Temp;
                City = weatherResponse.Name;
                Logger.LogInfo("Получил погоду");
            }
            catch
            {
                Temperature = 0;
                City = "Нет сети :(";
                Logger.LogError("Не удалось получить погоду. Нет сети.");
            }
        }

    }
}
