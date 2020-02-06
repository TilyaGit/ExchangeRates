using System;

namespace KMFService.Core
{ 
    public class Currency
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Code { get; set; }
        public float Value { get; set; }
        public DateTime Date { get; set; }
    }
}
