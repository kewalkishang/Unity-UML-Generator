using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using System.Linq;

//Class to handle handling parsing the type details and return it as a Class obj
public class ParseClass 
{

    static List<Type> associateClasses;
    public static Class getClassFromType(Type type)
    {
        try
        {

            Type classType = type;

            if(classType.IsArray)
            {
                classType = classType.GetElementType();
            }

            associateClasses = new List<Type>(); 
            //Get name;
            string classname = classType.Name;
            //Get attributes
            Attribute[] attributes = extractAttributes(classType);
            //Get Methods
            Operation[] operations = extractMethods(classType);    
            //Instantiate class 
            Class classIns = new Class(classType, classname, attributes, operations);

            //Get inherited classes
            classIns.inherited = getInheritedClasses(classType);    

            //Get interfaces implemented
            Type[] interfaces = classType.GetInterfaces();
            List<Type> inter = new List<Type>();
            for (int i = 0; i < interfaces.Length; i++)
            {
                if (!interfaces[i].Namespace.StartsWith("System"))
                {
                    if(interfaces[i].IsArray)
                    {
                        inter.Add(interfaces[i].GetElementType());
                    }
                    else
                    {
                        inter.Add(interfaces[i]);
                    }
                    
                }
            }
            classIns.implemented = inter;

            //Get associated classes
            classIns.associated = associateClasses;

            return classIns;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;


    }

    static Attribute[] extractAttributes(Type classType)
    {
        FieldInfo[] fields = classType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        Attribute[] attributes = new Attribute[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo property = fields[i];
            Visiblity accessModifier = GetAccessModifierUML(property.Attributes);
            string name = property.Name;
            Type type = property.FieldType;
            if (type.IsGenericType)
            {
                Type[] genericArguments = type.GetGenericArguments();
                foreach (Type type1 in genericArguments)
                {
                    //Type generic = genericArguments[0];
                    if (!IsBuiltInType(type1) && !IsUnityType(type1))
                    {
                        if (type1.IsArray)
                        {

                            associateClasses.Add(type1.GetElementType());
                        }
                        else
                        {
                            associateClasses.Add(type1);
                        }         
                    }      
                }
            }
            else
            if (!IsBuiltInType(type) && !IsUnityType(type))
            {
      
                if (type.IsArray)
                {
                    associateClasses.Add(type.GetElementType());
                }
                else
                {
                    associateClasses.Add(type);
                }
            }
            attributes[i] = new Attribute(accessModifier, name, type);
        }
        return attributes;
    }


    static Operation[] extractMethods(Type classType)
    {
        MethodInfo[] methods = classType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        Operation[] methodSignatures = new Operation[methods.Length];
        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo method = methods[i];
            Visiblity accessModifier = GetAccessModifierUML(method.Attributes);
            string name = method.Name;
            Type returnType = method.ReturnType;
            if (returnType.IsGenericType)
            {
                Type[] genericArguments = returnType.GetGenericArguments();
                foreach (Type type1 in genericArguments)
                {
                    //Type generic = genericArguments[0];
                    if (!IsBuiltInType(type1) && !IsUnityType(type1))
                    { 
                        if (type1.IsArray)
                        {
                            associateClasses.Add(type1.GetElementType());
                        }
                        else
                        {
                            associateClasses.Add(type1);
                        }
                    }
                    
                }

            }
            else
           if (!IsBuiltInType(returnType) && !IsUnityType(returnType))
            {

                if (returnType.IsArray)
                {
                    associateClasses.Add(returnType.GetElementType());
                }
                else
                {
                    associateClasses.Add(returnType);
                }
            }

            ParameterInfo[] parameterInfos = method.GetParameters();
            Parameter[] parameters = new Parameter[parameterInfos.Length];
            for (int j = 0; j < parameterInfos.Length; j++)
            {
                string paraName = parameterInfos[j].Name;
                Type paraType = parameterInfos[j].ParameterType;
                parameters[j] = new Parameter(paraType, paraName);
            }
            methodSignatures[i] = new Operation(accessModifier, returnType, name, parameters);
        }
        return methodSignatures;
    }

    static Visiblity GetAccessModifierUML(MethodAttributes attributes)
    {
        if ((attributes & MethodAttributes.Public) != 0)
        {
            return Visiblity.Public;
        }
        else if ((attributes & MethodAttributes.Family) != 0)
        {
            return Visiblity.Protected;
        }
        else if ((attributes & MethodAttributes.Private) != 0)
        {
            return Visiblity.Private;
        }
        else
        {
            return Visiblity.Package;
        }

    }


    public static bool CheckForAssociation(Type typeA)
    {
        PropertyInfo[] properties = typeA.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            Type returnType = property.PropertyType;
            if (!IsBuiltInType(returnType) && !IsUnityType(returnType) && !IsInterfaceOfGenericType(returnType))
            {
                return true;
            } 
        }

        return false;
    }


    public static bool IsBuiltInType(Type type)
    {
        return type.IsPrimitive || type.FullName.StartsWith("System") || type == typeof(void) || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(Type) || type.IsGenericType || type.IsGenericTypeDefinition;
    }


    public static bool IsInterfaceOfGenericType(Type genericType)
    {

        Type interfaceType = typeof(IEnumerable<>);
        if (!interfaceType.IsInterface || !genericType.IsGenericType)
        {
            return false;
        }

        Type genericTypeDef = genericType.GetGenericTypeDefinition();

        return interfaceType == genericTypeDef;
    }

    public static bool IsUnityType(Type type)
    {
        string unityAssemblyName = "UnityEngine";
        return type.Assembly.FullName.StartsWith(unityAssemblyName) || type.Assembly.FullName.StartsWith("System");
    }


    public static bool IsBuiltInInterface(Type interfaceType)
    {
        Assembly mscorlib = Assembly.GetAssembly(typeof(object));
        Assembly systemCore = Assembly.GetAssembly(typeof(Enumerable));

        string interfaceAssemblyName = interfaceType.Assembly.GetName().Name;
        string interfaceNamespace = interfaceType.Namespace;

        return interfaceAssemblyName == mscorlib.GetName().Name || interfaceAssemblyName == systemCore.GetName().Name;
    }

    static List<Type> getInheritedClasses(Type classType)
    {
        List<Type> classes = new List<Type>();
        Type currentType = classType;

        while (currentType.BaseType != null && currentType.BaseType.Namespace != "System")
        {
            currentType = currentType.BaseType;
            classes.Add(currentType);
        }

        return classes;
    }

    static Visiblity GetAccessModifierUML(FieldAttributes attributes)
    {
        if ((attributes & FieldAttributes.Public) != 0)
        {
            return Visiblity.Public;
        }
        else if ((attributes & FieldAttributes.Family) != 0)
        {
            return Visiblity.Protected;
        }
        else if ((attributes & FieldAttributes.Private) != 0)
        {
            return Visiblity.Private;
        }
        else
        {
            return Visiblity.Package;
        }
    }


}
