using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClassComponents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TestVisibility();
        //TestParameter();
        //TestAttribute();
        //TestOperation();
        TestClass();
    }

    void TestVisibility()
    {
        Debug.Log(Visiblity.Public);
        Debug.Log((char)Visiblity.Public);
    }

    void TestParameter()
    {
        Parameter para = new Parameter( typeof(int), "para");
        Debug.Log(para.ToString());
    }

    void TestAttribute()
    {
        Attribute attribute = new Attribute(Visiblity.Protected, "studentName", typeof(string));
        Debug.Log(attribute.ToString());
       
    }

    void TestOperation()
    {
        Parameter[] parameters = new Parameter[3];
        parameters[0] = new Parameter(typeof(int), "para");
        parameters[1] = new Parameter(typeof(string), "para1");
        parameters[2] = new Parameter(typeof(double), "para2");
        Operation operation = new Operation(Visiblity.Protected, typeof(string), "studentName" , parameters);
        Debug.Log(operation.ToString());

    }

    void TestClass()
    {
        Attribute[] attributes = new Attribute[3];
        attributes[0] = new Attribute(Visiblity.Protected, "studentName", typeof(string));
        attributes[1] = new Attribute(Visiblity.Public, "studentName1", typeof(string));
        attributes[2] = new Attribute(Visiblity.Private, "studentName2", typeof(string));

        Operation[] operations = new Operation[2];
        Parameter[] parameters = new Parameter[3];
        parameters[0] = new Parameter(typeof(int), "para");
        parameters[1] = new Parameter(typeof(string), "para1");
        parameters[2] = new Parameter(typeof(double), "para2");
        operations[0] = new Operation(Visiblity.Protected, typeof(string), "studentNamea", parameters);
        operations[1] = new Operation(Visiblity.Public, typeof(string), "studentNameb", parameters);
        // Debug.Log(operation.ToString());

        Class classInst = new Class( typeof(Class),"DemoClass", attributes, operations);
        Debug.Log(classInst.ToString());
    }
}
