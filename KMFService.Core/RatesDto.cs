using System.Xml.Serialization;

namespace KMFService.Core
{
    [XmlRoot]
    public class RatesDto
    {
        [XmlElement(ElementName = "generator")]
        public string Generator { get; set; }

        [XmlElement(ElementName = "item")]
        public CurrencyDto[] Currencies { get; set; }
    }

    public class  CurrencyDto
    { 
        [XmlElement(ElementName = "fullname")]
        public string Fullname { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "description")]
        public float Description { get; set; }

        [XmlElement(ElementName = "quant")]
        public int Quant { get; set; }

        [XmlElement(ElementName = "index")]
        public string Index { get; set; }

        [XmlElement(ElementName = "change")]
        public string Change { get; set; }
    }
}
