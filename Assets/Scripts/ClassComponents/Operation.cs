using System;

public class Operation
{
    Visiblity visiblity;
    Type returnType;
    string name;
    Parameter[] parameters;

    public Operation(Visiblity visiblity, Type returnType, string name, Parameter[] parameters)
    {
        this.visiblity = visiblity;
        this.returnType = returnType;
        this.name = name;
        this.parameters = parameters;
    }

    public string ToUMLStandard()
    {
        string parameterString = "";
        for (int i = 0; i < parameters.Length; i++)
        {
            parameterString += parameters[i].ToUMLStandard() + ", ";
        }

        return (char)visiblity + " " + name + "( "+ parameterString + ") : " + returnType.Name;
    }

    public override string ToString()
    {
        string parametersString = "";
        for(int i = 0; i < parameters.Length; i++)
        {
            parametersString += parameters[i].ToString() + "\n";
        }
        return "Operation (" + visiblity.ToString() + " : " + returnType.Name + " : " + name + " : " +
                          parametersString + " )";
    }
}
