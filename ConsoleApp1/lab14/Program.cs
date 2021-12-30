using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Text.Json;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            //ЗАДАНИЕ 1

            // Сериализация и десереализация в/из бинарный формат
            Document doc1 = new Document("Магнит", 12, 12, 2020, true);
            Document doc2 = new Document("Себастьян", 10, 11, 2019, false);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(@"D:\C#\lab14\lab14\document1.dat", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fs, doc1);
            }
            using (FileStream fs = new FileStream(@"D:\C#\lab14\lab14\document1.dat", FileMode.OpenOrCreate))
            {
                Document newdoc1 = (Document)binaryFormatter.Deserialize(fs);
                Console.WriteLine($"название организации – {newdoc1.NameOfOrganization}, дата – {newdoc1.Day}.{newdoc1.Month}.{newdoc1.Year}, подписан? - {newdoc1.IsStamped}");
            }

            // Сериализация и десереализация в/из формат SOAP
            SoapFormatter soapFormatter = new SoapFormatter();
            using (FileStream fStream = new FileStream(@"D:\C#\\lab14\lab14\document2Soap.soap", FileMode.OpenOrCreate))
            {
                soapFormatter.Serialize(fStream, doc1);
            }
            using (FileStream fStream = new FileStream(@"D:\C#\lab14\lab14\document2Soap.soap", FileMode.OpenOrCreate))
            {
                Document newdoc2 = (Document)soapFormatter.Deserialize(fStream);
                Console.WriteLine($"название организации – {newdoc2.NameOfOrganization}, дата – {newdoc2.Day}.{newdoc2.Month}.{newdoc2.Year}, подписан? - {newdoc2.IsStamped}");
            }

            // Сериализация и десереализация в/из формат JSON

            string jsonString = JsonSerializer.Serialize(doc1);
            using (StreamWriter sw = new StreamWriter(@"D:\C#\lab14\lab14\document.json", false))
            {
                sw.Write(jsonString);
            }
            Document newdocument = JsonSerializer.Deserialize<Document>(jsonString);
            Console.WriteLine($"название организации – {newdocument.NameOfOrganization}, подписан? - {newdocument.IsStamped}");

            // Сериализация и десереализация в/из формат XML

            XmlSerializer xSer = new XmlSerializer(typeof(Document));
            using (FileStream fs = new FileStream(@"D:\C#\lab14\lab14\document.xml", FileMode.OpenOrCreate))
            {
                xSer.Serialize(fs, doc1);
            }
            using (FileStream fs = new FileStream(@"D:\C#\lab14\lab14\document.xml", FileMode.OpenOrCreate))
            {
                Document newdoc = xSer.Deserialize(fs) as Document;
                Console.WriteLine($"название организации – {newdoc.NameOfOrganization}, дата – {newdoc.Day}.{newdoc.Month}.{newdoc.Year}, подписан? - {newdoc.IsStamped}");
            }

            // ЗАДАНИЕ 2 - СЕРИАЛИЗАЦИЯ/ДЕСЕРЕАЛИЗАЦИЯ МАССИВА

            Document[] documents = { doc1, doc2 };
            using (FileStream fs = new FileStream(@"D:\C#\lab14\lab14\documents.dat", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fs, documents);
            }
            using (FileStream fs = new FileStream(@"D:\C#\lab14\lab14\documents.dat", FileMode.OpenOrCreate))
            {
                Document[] newdocuments = (Document[])binaryFormatter.Deserialize(fs);
                Console.WriteLine(newdocuments.Length);
            }

            // ЗАДАНИЕ 3 - СЕЛЕКТОРЫ ДЛЯ XML ФАЙЛА

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"D:\C#\lab14\lab14\document.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            // выбор всех дочерних узлов
            XmlNodeList childnodes = xRoot.SelectNodes("*");
            foreach (XmlNode n in childnodes)
                Console.WriteLine(n.InnerText);

            XmlNode stampNode = xRoot.SelectSingleNode("stamp");
            if (stampNode != null)
                Console.WriteLine(stampNode.OuterXml);

            // ЗАДАНИЕ 4 - LINQ TO XML, СОЗДАНИЕ XML-ФАЙЛОВ

            XDocument xdoc = new XDocument();

            XElement parrot1 = new XElement("parrot");
            XAttribute parrot1NameAttr = new XAttribute("name", "Mike");
            XElement parrot1ColorAttr = new XElement("color", "white");
            parrot1.Add(parrot1NameAttr);
            parrot1.Add(parrot1ColorAttr);

            XElement parrot2 = new XElement("parrot");
            XAttribute parrot2NameAttr = new XAttribute("name", "Bee");
            XElement parrot2ColorAttr = new XElement("color", "red");
            parrot2.Add(parrot2NameAttr);
            parrot2.Add(parrot2ColorAttr);

            // создание корневого элемента
            XElement parrots = new XElement("parrots");
            parrots.Add(parrot1);
            parrots.Add(parrot2);
            // добавляем корневой элемент в документ
            xdoc.Add(parrots);
            xdoc.Save(@"D:\C#\lab14\lab14\parrots.xml");

            XDocument xdoc1 = XDocument.Load(@"D:\C#\lab14\lab14\parrots.xml");
            IEnumerable<XElement> elements = xdoc1.Descendants("parrot");
            foreach (XElement e in elements)
                Console.WriteLine($"Элемент {e.Name}, цвет {e.Value}");

            IEnumerable<XElement> all = xdoc1.Root.Descendants("parrot")
                                                  .Where(p => p.Element("color").Value.StartsWith("r"))
                                                  .Select(p => p);
            foreach (XElement e in all)
                Console.WriteLine($"Элемент {e.Name}, цвет {e.Value}");

            Console.ReadKey();
        }
    }
}