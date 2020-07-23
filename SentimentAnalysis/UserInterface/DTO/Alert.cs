using System;

namespace UserInterface.DTO
{
    [Serializable]
    public class Alert
    {
        public string InitDate { get; set; }
        public string NumberDays { get; set; }
        public string SentimentType { get; set; }
        public string CantPost { get; set; }
        public string Entity { get; set; }
        public string Authors { get; set; }
    }
}
