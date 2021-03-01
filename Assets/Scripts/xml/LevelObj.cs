using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class LevelObj
{
    public ObjType type;
    public float x;
    public float y;
}

public enum ObjType
{
    Normal,
    Stone,
    Coin
}