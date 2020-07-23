using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.DTO
{
    [Serializable]
    public class Analysis
    {
        public string Author { get; set; }
        public string Word { get; set; }
        public string SentimentType { get; set; }
        public string Entity { get; set; }
        public string PostedDate { get; set; }
        public string Grade { get; set; }
    }
}
