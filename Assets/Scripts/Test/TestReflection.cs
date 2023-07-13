using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class TestReflection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("START: ");
        try
        {

          // Debug.Log("TRY: " );
            // Provide the full name of the class (including namespace if applicable)
           string className = "BasicClass";

            // Load the assembly containing the class
            Assembly assembly = typeof(TestReflection).Assembly; // or Assembly.LoadFrom("path/to/YourAssembly.dll")

            // Get the type information of the class
            Type classType = assembly.GetType(className);
            //  PrintInheritedClasses(classType);
            Type asso = typeof(BankAccount);
            //Debug.Log("Asso : " + asso.Name);
            //List <Type> types = AssociationAnalyzer.FindAssociations(classType);
            //Debug.Log("Association : " + asso.Name + " : "  +types.Count);
            //foreach (Type type in types)
            //{
            //    Debug.Log(type.Name);
            //}


            Type circleType = typeof(Circle);
            Type rectangleType = typeof(Rectangle);
            Debug.Log("Implementation : " + circleType.Name);
            Debug.Log("Implementation : " + rectangleType.Name);
            bool isCircleImplementingShape = circleType.GetInterface(nameof(IShape)) != null;
            bool isRectangleImplementingShape = rectangleType.GetInterface(nameof(IShape)) != null;

            Debug.Log("Circle implements IShape: " + isCircleImplementingShape);
            Debug.Log("Rectangle implements IShape: " + isRectangleImplementingShape);

            Type[] interfaces = circleType.GetInterfaces();
            Debug.Log("Interfaces ");
            foreach (Type interfaceType in interfaces)
            {
                Debug.Log("Implemented Interface: " + interfaceType.FullName);
            }

            // Extract class information
            //    string classFullName = classType.FullName;
            //    string classNamespace = classType.Namespace;
            //    string classDescription = classType.ToString();
            //    string[] attributes = extractAttributes(classType);
            //    string[] methods = extractMethods(classType);
            //    string[] fields = extractFields(classType);

            //    // Print class information
            //    Debug.Log("Class: " + classFullName);
            //    Debug.Log("Namespace: " + classNamespace);
            //    Debug.Log("Description: " + classDescription);
            //    Debug.Log("Attributes: " + string.Join(", ", attributes));
            //    Debug.Log("Methods: " + string.Join(", ", methods));
            //    Debug.Log("Fields: " + string.Join(", ", fields));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static string[] extractAttributes(Type classType)
    {
        PropertyInfo[] properties = classType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        string[] attributes = new string[properties.Length];
        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
            string accessModifier = GetAccessModifier(property.GetGetMethod(true).Attributes);
            string name = property.Name;
            string type = property.PropertyType.Name;
            attributes[i] = accessModifier + " " + type + " " + name;
        }
        return attributes;
    }

    private static string[] extractMethods(Type classType)
    {
        MethodInfo[] methods = classType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        string[] methodSignatures = new string[methods.Length];
        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo method = methods[i];
            string accessModifier = GetAccessModifier(method.Attributes);
            string name = method.Name;
            string returnType = method.ReturnType.Name;
            methodSignatures[i] = accessModifier + " " + returnType + " " + name;
        }
        return methodSignatures;
    }

    private static string[] extractFields(Type classType)
    {
        FieldInfo[] fields = classType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        string[] fieldSignatures = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo field = fields[i];
           // string accessModifier = GetAccessModifier(method.Attributes);
            string name = field.Name;
            Type fieldType = field.FieldType;
            fieldSignatures[i] = name + " " + fieldType.Name;
        }
        return fieldSignatures;
    }

    private static void PrintInheritedClasses(Type classType)
    {
        Type currentType = classType;

        while (currentType.BaseType != null)
        {
            currentType = currentType.BaseType;
            Debug.Log(currentType.FullName);
        }
    }


    private static string GetAccessModifier(MethodAttributes attributes)
    {
        if ((attributes & MethodAttributes.Public) != 0)
        {
            return "public";
        }
        else if ((attributes & MethodAttributes.Family) != 0)
        {
            return "protected";
        }
        else if ((attributes & MethodAttributes.Private) != 0)
        {
            return "private";
        }
        else
        {
            return "default";
        }
    }


    private static string GetAccessModifier(FieldAttributes attributes)
    {
        if ((attributes & FieldAttributes.Public) != 0)
        {
            return "public";
        }
        else if ((attributes & FieldAttributes.Family) != 0)
        {
            return "protected";
        }
        else if ((attributes & FieldAttributes.Private) != 0)
        {
            return "private";
        }
        else
        {
            return "default";
        }
    }

}
