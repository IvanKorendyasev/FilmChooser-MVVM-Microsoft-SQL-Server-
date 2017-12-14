using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class FilmModel
    {
        public string Firm { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int ISO { get; set; }
        public int Cost { get; set; }
       
    }
}
