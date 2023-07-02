using System;

public class Attribute
{
    Visiblity visiblity;
    string name;
    Type type;

    public Attribute(Visiblity visiblity, string name, Type type)
    {
        this.visiblity = visiblity;
        this.name = name;
        this.type = type;
    }

    public override string ToString()
    {
        return "Attribute ( " + visiblity.ToString()  + " : " + type.ToString() + " : " + name + " )";
            
    }
}
