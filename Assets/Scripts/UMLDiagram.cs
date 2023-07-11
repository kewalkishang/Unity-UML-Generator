using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class UMLDiagram : MonoBehaviour
{
    List<Class> classes = new List<Class>();
    List<Type> classAssociated = new List<Type>();
    public ClassDiagram classDiagram;
    public Canvas canvasObj;

    public Sprite associatedSprite;
    public Sprite inheritedSprite;
    public Sprite implementedSprite;
    //public GameObject line;
    //public GameObject img;
    public GameObject associationPrefab;

    Attribute[] extractAttributes(Type classType)
    {
        FieldInfo[] fields = classType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        //string[] attributes = new string[properties.Length];
        Attribute[] attributes = new Attribute[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo property = fields[i];
            Visiblity accessModifier = GetAccessModifierUML(property.Attributes);
            string name = property.Name;
            Type type = property.FieldType;
            attributes[i] = new Attribute(accessModifier, name, type);
        }
        return attributes;
    }


    Operation[] extractMethods(Type classType)
    {
        MethodInfo[] methods = classType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        Operation[] methodSignatures = new Operation[methods.Length];
        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo method = methods[i];
            Visiblity accessModifier = GetAccessModifierUML(method.Attributes);
            string name = method.Name;
            Type returnType = method.ReturnType;
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


    private static List<Type> getInheritedClasses(Type classType)
    {
        List<Type> classes = new List<Type>();
        Type currentType = classType;

        while (currentType.BaseType != null && currentType.BaseType.Namespace != "System")
        {
            currentType = currentType.BaseType;
          //  Debug.Log(currentType.FullName);
            classes.Add(currentType);
        }

        return classes;
    }

    Class DrawClass(Type typec)
    {
        try
        {
            
            // Debug.Log("TRY: " );
            // Provide the full name of the class (including namespace if applicable)
           

            Type classType = typec;
            //Get name;
            string classname = classType.Name;

            //Get fields;
            // Fiel
            Attribute[] attributes = extractAttributes(classType);
            //Get Methods

            Operation[] operations = extractMethods(classType);

            //Instantiate class 
            Class classIns = new Class(classname, attributes, operations);


            classIns.inherited = getInheritedClasses(classType);
            foreach (Type type in classIns.inherited)
            {
                classAssociated.Add(type);

            }
           // Debug.Log(classIns.inherited[0].Name);

            Type[] interfaces = classType.GetInterfaces();
            List<Type> inter = new List<Type>();
            for (int i = 0; i < interfaces.Length; i++)
            {
                inter.Add(interfaces[i]);
                classAssociated.Add(interfaces[i]);
                //classes.Add(interfaces[i]);
            }

            classIns.implemented = inter;
           // Debug.Log(classIns.implemented[0].Name);

            //Need something for association
        

            return classIns;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;

    }

    List<GameObject> panels = new List<GameObject>();
    Dictionary<Type, List<Type>> association = new Dictionary<Type, List<Type>>();
    Dictionary<Type, List<Type>> inherited = new Dictionary<Type, List<Type>>();
    Dictionary<Type, List<Type>> implemented = new Dictionary<Type, List<Type>>();
    Dictionary<Type, GameObject> typePanel = new Dictionary<Type, GameObject>();
    //List<GameObject, GameObject>  
    // Start is called before the first frame update
    void Start()
    {
        string className = "BasicClass";

        // Load the assembly containing the class
        Assembly assembly = typeof(TestReflection).Assembly; // or Assembly.LoadFrom("path/to/YourAssembly.dll")

        // Get the type information of the class
        Type classType = assembly.GetType(className);
        classAssociated.Add(classType);
        while (classAssociated.Count > 0)
        {
            Type ty = classAssociated[0];
            Class classIns = DrawClass(ty);
           // Debug.Log("Class " + classIns.ToString());
            classAssociated.Remove(ty);
            association.Add(ty, new List<Type>());
            association[ty].AddRange(classIns.associated);

            inherited.Add(ty, new List<Type>());
            inherited[ty].AddRange(classIns.inherited);

            implemented.Add(ty, new List<Type>());
            implemented[ty].AddRange(classIns.implemented);
            GameObject panel =  classDiagram.CreateNewDiagram(classIns);
            //panel.AddComponent<DraggablePanel>();
            typePanel.Add(ty, panel);
            panel.GetComponent<DraggablePanel>().canvas = canvasObj;
            panels.Add(panel);
        }

     
            Invoke("GetRectSet", 0.01f);
            // Set the new X-axis position (POSX)
            //anchoredPosition.x = 200 * i;

            // Update the anchored position of the panel
            //panels[i].GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        
    }

    float left = 0;
    float right = 0;
    float top = 0;
    float bottom = 0;
    float offset = 50; 
    //This is the space out the individual class diagrams - not overlapping.
    void GetRectSet()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            Vector2 anchoredPosition = panels[i].GetComponent<RectTransform>().anchoredPosition;
           // Debug.Log("ANCHOR W" + panels[i].GetComponent<RectTransform>().rect.width);
            //Debug.Log("ANCHOR H" + panels[i].GetComponent<RectTransform>().rect.height);
            anchoredPosition.x =  right + offset;
            anchoredPosition.y = bottom + offset;
            // anchoredPosition.y = ;
            panels[i].GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            right += panels[i].GetComponent<RectTransform>().rect.width;
            bottom += panels[i].GetComponent<RectTransform>().rect.height/ 2;
            //Set the Layout panels[i].GetComponent<RectTransform>().rect.widthMin Value equivaline to RectTransform
            //Update last values
        }
        //   this.GetComponent<LayoutElement>().minHeight = this.GetComponent<RectTransform>().rect.height;
       addLinks();

    }

    void addLinks()
    {
        Debug.Log("addlinks " + typePanel.Count  + " : "+ typePanel.ToString());
        foreach(Type type in association.Keys)
        {
           // Debug.Log("From " + );
            List <Type> tolist = association[type];
            foreach(Type to in tolist)
            {

                Debug.Log("From "+ type.ToString() + " : to : " + to.ToString());
                GameObject link = Instantiate(associationPrefab);
                AssociationLine assline = link.GetComponent<AssociationLine>();
                assline.panel = typePanel[type];
                assline.panel2 = typePanel[to];
                assline.LinkSprite = associatedSprite;
                assline.Initialize();
            }

        }

        foreach (Type type in inherited.Keys)
        {
            // Debug.Log("From " + );
            List<Type> tolist = inherited[type];
            foreach (Type to in tolist)
            {

                Debug.Log("From " + type.ToString() + " : to : " + to.ToString());
                GameObject link = Instantiate(associationPrefab);
                AssociationLine assline = link.GetComponent<AssociationLine>();
                assline.panel = typePanel[type];
                assline.panel2 = typePanel[to];
                assline.LinkSprite = inheritedSprite;
                assline.Initialize();
            }

        }


        foreach (Type type in implemented.Keys)
        {
            // Debug.Log("From " + );
            List<Type> tolist = implemented[type];
            foreach (Type to in tolist)
            {

                Debug.Log("From " + type.ToString() + " : to : " + to.ToString());
                GameObject link = Instantiate(associationPrefab);
                AssociationLine assline = link.GetComponent<AssociationLine>();
                assline.panel = typePanel[type];
                assline.panel2 = typePanel[to];
                assline.LinkSprite = implementedSprite;
                assline.Initialize();
            }

        }

    }

    Visiblity GetAccessModifierUML(MethodAttributes attributes)
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

    Visiblity GetAccessModifierUML(FieldAttributes attributes)
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
