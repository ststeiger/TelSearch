
using System.Linq;


#if false  


namespace TelSearch
{


    public class DynamicXml : System.Dynamic.DynamicObject // In System.Core.dll
    {


        public static void Test()
        {
            string xml = @"
<Students>
<Student ID=""100"">
    <Name>Arul</Name>
    <Mark>90</Mark>
</Student>
<Student>
    <Name>Arul2</Name>
    <Mark>80</Mark>
</Student>
</Students>
";

            //Requires Microsoft.CSharp.dll
            dynamic students = DynamicXml.Parse(xml);

            // var id = students.Student[0].ID;
            // var name1 = students.Student[1].Name;

            foreach (var std in students.Student)
            {
                System.Console.WriteLine(std.Mark);
            }
            
        }



        System.Xml.Linq.XElement _root;
        private DynamicXml(System.Xml.Linq.XElement root)
        {
            _root = root;
        } // End Constructor


        public static DynamicXml Parse(string xmlString)
        {
            return new DynamicXml(System.Xml.Linq.XDocument.Parse(xmlString).Root);
        } // End Function Parse


        public static DynamicXml Load(string filename)
        {
            return new DynamicXml(System.Xml.Linq.XDocument.Load(filename).Root);
        } // End Function Load


        public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
        {
            result = null;

            var att = _root.Attribute(binder.Name);
            if (att != null)
            {
                result = att.Value;
                return true;
            } // End if (att != null)

            var nodes = _root.Elements(binder.Name);
            
            if (nodes.Count() > 1)
            {
                result = nodes.Select(n => new DynamicXml(n)).ToList();
                return true;
            } // End if (nodes.Count() > 1)

            var node = _root.Element(binder.Name);
            if (node != null)
            {
                if (node.HasElements)
                {
                    result = new DynamicXml(node);
                }
                else
                {
                    result = node.Value;
                }

                return true;
            } // End if (node != null)

            return true;
        } // End Function TryGetMember


    } // End Class DynamicXml


} // End Namespace TelSearch

#endif
