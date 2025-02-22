using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class CodingSession
    {
        public int Id { get; set; }
        public string Start {  get; set; }
        public string End { get; set; }
        public string Duration { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Start: {Start}, End: {End}, Duration: {Duration}";
        }

    }
}
