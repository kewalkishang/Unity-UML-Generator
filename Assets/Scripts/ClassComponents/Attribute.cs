using System;

public class Attribute
{
    Visiblity visiblity;
    string name;
    public Type type;

    public Attribute(Visiblity visiblity, string name, Type type)
    {
        this.visiblity = visiblity;
        this.name = name;
        this.type = type;
    }

    public string ToUMLStandard()
    {
        return (char)visiblity + " " + name + " : " + type.Name;

    }

    public override string ToString()
    {
        return "Attribute ( " + visiblity.ToString()  + " : " + type.Name + " : " + name + " )";
            
    }
}
