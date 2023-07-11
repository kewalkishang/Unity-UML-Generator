using System;
using System.Collections.Generic;
using System.Reflection;

public static class AssociationAnalyzer
{
    public static List<Type> FindAssociations(Type targetClass)
    {
        List<Type> associations = new List<Type>();

        // Analyze fields
        FieldInfo[] fields = targetClass.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo field in fields)
        {
            Type fieldType = field.FieldType;
            if (IsAssociation(fieldType) && !associations.Contains(fieldType))
            {
                associations.Add(fieldType);
            }
        }

        // Analyze properties
        PropertyInfo[] properties = targetClass.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (PropertyInfo property in properties)
        {
            Type propertyType = property.PropertyType;
            if (IsAssociation(propertyType) && !associations.Contains(propertyType))
            {
                associations.Add(propertyType);
            }
        }

        // Analyze method parameters
        MethodInfo[] methods = targetClass.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (MethodInfo method in methods)
        {
            ParameterInfo[] parameters = method.GetParameters();
            foreach (ParameterInfo parameter in parameters)
            {
                Type parameterType = parameter.ParameterType;
                if (IsAssociation(parameterType) && !associations.Contains(parameterType))
                {
                    associations.Add(parameterType);
                }
            }
        }

        return associations;
    }

    private static bool IsAssociation(Type type)
    {
        // Check if the type represents an association.
        // You can define your own logic here based on your specific criteria.
        // For example, you may check for specific attributes, interfaces, naming conventions, etc.

        // In this example, we consider any non-system type as an association.
        //return !type.Namespace.StartsWith("System");
        return true;
    }
}
