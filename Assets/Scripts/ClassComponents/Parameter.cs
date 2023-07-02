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

    public override string ToString()
    {
        return "Parameter ( " + type.ToString() + " : " + name + " )";
    }
}
