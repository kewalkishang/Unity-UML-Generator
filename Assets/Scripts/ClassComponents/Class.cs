using System;
using System.Collections.Generic;

public class Class
{
    public string name;
    public Attribute[] attributes;
    public Operation[] operations;
    public List<Type> inherited = new List<Type>();
    public List<Type> associated = new List<Type>();
    public List<Type> implemented = new List<Type>();

    public Class(string name, Attribute[] attributes, Operation[] operations)
    {
        this.name = name;
        this.attributes = attributes;
        this.operations = operations;
    }


    public override string ToString()
    {
        string attributeString = "";
        for( int i = 0; i < attributes.Length; i++)
        {
            attributeString += attributes[i].ToString() + "\n";
        }

        string operationsString = "";
        for (int i = 0; i < operations.Length; i++)
        {
            operationsString += operations[i].ToString() + "\n";
        }

        return "Class (" + name + " : " + attributeString + " : " + operationsString  + ")";

    }
}
