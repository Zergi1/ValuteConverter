using System;
using System.Collections.Generic;

namespace ValuteConverter.AppData
{
    public class Cource
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, Valute> Valute { get; set; }
    }
}
