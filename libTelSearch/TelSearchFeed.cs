
// XML-Schema for SwissCom FixNet API @http://tel.search.ch/api/?
// Copyright © (C) 2014 Stefan Steiger
// All rights reserved / Alle Rechte vorbehalten. 

// Alle Dateien, sowie jegliche Teile davon, uterliegen dem Urheberrecht 
// und anderen Gesetzen zum Schutz geistigen Eigentums. 
// Sie dürfen weder für Handelszwecke oder zur Weitergabe kopiert, 
// noch patentiert, noch verändert und auf anderen Web-Sites verwendet werden. 
// Die Search.ch API unterliegt dem Urheberrecht derjenigen , die dieses zur Verfügung gestellt haben.


namespace TelSearch
{


    public class cLink
    {
        [System.Xml.Serialization.XmlAttribute]
        public string type { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string rel { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string href { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string title { get; set; }
    }


    // <generator version="1.0" uri="http://tel.search.ch">tel.search.ch</generator>
    public class cGenerator
    {
        [System.Xml.Serialization.XmlAttribute("version")]
        public string Version { get; set; }

        [System.Xml.Serialization.XmlAttribute("uri")]
        public string Uri { get; set; }

        [System.Xml.Serialization.XmlText]
        public string Value;
    }


    public class cAuthor
    {
        [System.Xml.Serialization.XmlElement("name")]
        public string Name;
    }


    public class cTitle
    {
        [System.Xml.Serialization.XmlAttribute("type")]
        public string Type;

        [System.Xml.Serialization.XmlText()]
        public string value;
    }


    public class cContent
    {
        [System.Xml.Serialization.XmlAttribute("type")]
        public string Type;

        [System.Xml.Serialization.XmlText()]
        public string value;
    }


    public class cEntry
    {
        [System.Xml.Serialization.XmlElement("id")]
        public string Id;

        [System.Xml.Serialization.XmlElement("updated")]
        public System.DateTime Updated;

        [System.Xml.Serialization.XmlElement("published")]
        public System.DateTime Published;

        [System.Xml.Serialization.XmlElement("title")]
        public cTitle Title;

        [System.Xml.Serialization.XmlElement("content")]
        public cContent content;

        [System.Xml.Serialization.XmlElement("author")]
        public cAuthor author;

        [System.Xml.Serialization.XmlElement("link")]
        public cLink[] Links; // { get; set; };


        [System.Xml.Serialization.XmlElement("pos", Namespace = cFeed.tel)]
        public int TelPos;

        [System.Xml.Serialization.XmlElement("id", Namespace = cFeed.tel)]
        public string TelId;

        [System.Xml.Serialization.XmlElement("type", Namespace = cFeed.tel)]
        public string type;

        [System.Xml.Serialization.XmlElement("name", Namespace = cFeed.tel)]
        public string name;

        [System.Xml.Serialization.XmlElement("firstname", Namespace = cFeed.tel)]
        public string firstname;

        [System.Xml.Serialization.XmlElement("maidenname", Namespace = cFeed.tel)]
        public string maidenname;

        [System.Xml.Serialization.XmlElement("street", Namespace = cFeed.tel)]
        public string street;

        [System.Xml.Serialization.XmlElement("streetno", Namespace = cFeed.tel)]
        public string streetno;

        [System.Xml.Serialization.XmlElement("zip", Namespace = cFeed.tel)]
        public string zip;

        [System.Xml.Serialization.XmlElement("city", Namespace = cFeed.tel)]
        public string city;

        [System.Xml.Serialization.XmlElement("canton", Namespace = cFeed.tel)]
        public string canton;

        [System.Xml.Serialization.XmlElement("nopromo", Namespace = cFeed.tel)]
        public string nopromo;

        [System.Xml.Serialization.XmlElement("phone", Namespace = cFeed.tel)]
        public string phone;


        [System.Xml.Serialization.XmlElement("slaveentry", Namespace = cFeed.tel)]
        public cSlaveEntry SlaveEntry;


        [System.Xml.Serialization.XmlElement("extra", Namespace = cFeed.tel)]
        public Xtra[] Extras; // { get; set; };


        [System.Xml.Serialization.XmlElement("businesslink", Namespace = cFeed.tel)]
        public string businesslink;

        [System.Xml.Serialization.XmlElement("copyright", Namespace = cFeed.tel)]
        public string copyright;


    }


    public class cSlaveEntry
    {
        [System.Xml.Serialization.XmlElement("occupation", Namespace = cFeed.tel)]
        public string Occupation;

        [System.Xml.Serialization.XmlElement("nopromo", Namespace = cFeed.tel)]
        public string NoPromo;

        [System.Xml.Serialization.XmlElement("extra", Namespace = cFeed.tel)]
        public Xtra[] Extras; // { get; set; };
    }


    public class Xtra
    {
        [System.Xml.Serialization.XmlAttribute("type")]
        public string Type;

        [System.Xml.Serialization.XmlText()]
        public string value;
    }


    //<openSearch:Query role="request" searchTerms="john meier " startPage="1" />
    public class OpenSearchQuery
    {
        [System.Xml.Serialization.XmlAttribute("role")]
        public string Role;

        [System.Xml.Serialization.XmlAttribute("searchTerms")]
        public string SearchTerms;

        [System.Xml.Serialization.XmlAttribute("startPage")]
        public int StartPage;
    }


    //<openSearch:Image height="1" width="1" type="image/gif">http://www.search.ch/audit/CP/tel/de/api</openSearch:Image>
    public class OpenSearchImage
    {
        [System.Xml.Serialization.XmlAttribute("width")]
        public int Width;

        [System.Xml.Serialization.XmlAttribute("height")]
        public int Height;

        [System.Xml.Serialization.XmlAttribute("type")]
        public string Type;


        [System.Xml.Serialization.XmlText]
        public string Url;
    }


    //[System.Xml.Serialization.XmlRoot(Namespace = "http://example.com/2007/ns1")]
    [System.Xml.Serialization.XmlRoot("feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class cFeed
    {
        [System.Xml.Serialization.XmlIgnore]
        public const string openSearch = "http://a9.com/-/spec/opensearchrss/1.0/";

        [System.Xml.Serialization.XmlIgnore]
        public const string tel = "http://tel.search.ch/api/spec/result/1.0/";


        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces();


        public cFeed()
        {
            xmlns.Add("openSearch", openSearch);
            xmlns.Add("tel", tel);
        }

        [System.Xml.Serialization.XmlElement("id")]
        public string Id;

        [System.Xml.Serialization.XmlElement("title")]
        public cTitle Title;

        [System.Xml.Serialization.XmlElement("generator")]
        public cGenerator Generator;

        [System.Xml.Serialization.XmlElement("updated")]
        public System.DateTime Updated;

        [System.Xml.Serialization.XmlElement("link")]
        public cLink[] Links; // { get; set; };


        // <tel:errorCode>403</tel:errorCode>
        [System.Xml.Serialization.XmlElement("errorCode", Namespace = cFeed.tel)]
        public string ErrorCode;

        // <tel:errorReason>Forbidden</tel:errorReason>
        [System.Xml.Serialization.XmlElement("errorReason", Namespace = cFeed.tel)]
        public string ErrorReason;

        // <tel:errorMessage>The submitted API-Key is invalid or blocked</tel:errorMessage>
        [System.Xml.Serialization.XmlElement("errorMessage", Namespace = cFeed.tel)]
        public string ErrorMessage;





        // Start OpenSearch
        [System.Xml.Serialization.XmlElement("totalResults", Namespace = openSearch)]
        public int totalResults;

        [System.Xml.Serialization.XmlElement("startIndex", Namespace = openSearch)]
        public int startIndex;

        [System.Xml.Serialization.XmlElement("itemsPerPage", Namespace = openSearch)]
        public int itemsPerPage;

        [System.Xml.Serialization.XmlElement("Query", Namespace = openSearch)]
        public OpenSearchQuery Query;

        [System.Xml.Serialization.XmlElement("Image", Namespace = openSearch)]
        public OpenSearchImage Image;
        // End OpenSearch


        [System.Xml.Serialization.XmlElement("entry")]
        public cEntry[] Entries; // { get; set; };

    } // cFeed


}
