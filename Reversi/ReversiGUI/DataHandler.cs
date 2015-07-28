using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGUI
{
    class DataHandler
    {
        public static readonly string MATRIX_FILE = "gameData.conf";
        public static void WriteData(string[] Grid)
        {
             System.IO.File.WriteAllLines(@"gameData.conf", Grid);
        }

        public static string[] ReadData()
        {
            return System.IO.File.ReadAllLines(@"gameData.conf");
        }
    }
}
