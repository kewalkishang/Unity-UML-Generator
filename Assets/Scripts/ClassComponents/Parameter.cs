using System;

public class Parameter
{
    Type type;
    string name;

    public Parameter(Type type, string name)
    {
        this.type = type;
        this.name = name;
    }

    public string ToUMLStandard()
    {
        return name + " : " + type.Name;
    }

    public override string ToString()
    {
        return "Parameter ( " + type.Name + " : " + name + " )";
    }
}
