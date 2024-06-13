using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Entry
    {
        public string Date { get; set; } = DateOnly.FromDateTime(DateTime.Now).ToString(CultureInfo.CurrentCulture);
        public string DoneToday { get; set; } = "";
        public string DoneWell { get; set; } = "";
        public string DoneBad { get; set; } = "";
        public string Improvements { get; set; } = "";
        public int StarScale { get; set; } = 0;
    }
}
