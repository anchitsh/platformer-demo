﻿using System.Collections.Generic;
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

}
