using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("TileCollection")]
public class ReadXML 
{
    [XmlArray("TileObjects"), XmlArrayItem("TileObj")]
    public LevelObj[] TileObjects;

    public static ReadXML Load(string path)
    {
        var serializer = new XmlSerializer(typeof(ReadXML));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as ReadXML;
        }
    }

    public static ReadXML LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(ReadXML));
        return serializer.Deserialize(new StringReader(text)) as ReadXML;
    }
    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(ReadXML));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }
}
