using System;

namespace KMFService.Core
{ 
    public class Currency
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }
    }
}
