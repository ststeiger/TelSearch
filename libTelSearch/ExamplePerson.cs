
using System;
using System.Collections.Generic;
using System.Text;



//using Microsoft.VisualBasic;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;

namespace TelSearch
{



    [System.Xml.Serialization.XmlRoot(ElementName = "person")]
    public class Person
    {

        [System.Xml.Serialization.XmlElement(Namespace = "http://example.com")]
        public string fname = "myfname";

        [System.Xml.Serialization.XmlElement(Namespace = "http://sample.com")]
        public string lname = "mylname";

        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces();

        public Person()
        {
            xmlns.Add("a", "http://example.com");
            xmlns.Add("b", "http://sample.com");
        }
    }




}


// http://www.search.ch/jobs/engineer.html