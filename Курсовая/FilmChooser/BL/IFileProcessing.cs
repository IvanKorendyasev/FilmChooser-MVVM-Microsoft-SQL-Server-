using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IFileProcessing
    {
        float Temperature { get; set; }
        List<Film> FilmsFromDB { get; set; }
        string City { get; set; }
        void GoToSite();
        void SaveInExcel(List<Film> FilmsToSave);
        List<Film> GetFilmsFromDB();
        void GetTempFromAPI();

    }
}
