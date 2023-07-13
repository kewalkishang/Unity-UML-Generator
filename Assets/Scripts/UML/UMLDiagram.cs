using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection; //API to programmatically investigate assemblies.

//Class to create the UML class diagram and their relationships
public class UMLDiagram : MonoBehaviour
{
    List<Class> classes = new List<Class>();
    List<Type> classAssociated = new List<Type>();
    public ClassDiagram classDiagram;
    public Canvas canvasObj;

    public Sprite associatedSprite;
    public Sprite inheritedSprite;
    public Sprite implementedSprite;

    //This is the first class which is recursively traversed to find connected classes.
    public string StartClass = "BasicClass";
    public GameObject associationPrefab;


    List<GameObject> panels = new List<GameObject>();
    Dictionary<Type, List<Type>> association = new Dictionary<Type, List<Type>>();
    Dictionary<Type, List<Type>> inherited = new Dictionary<Type, List<Type>>();
    Dictionary<Type, List<Type>> implemented = new Dictionary<Type, List<Type>>();

    //Map the type to the UIPanels
    Dictionary<Type, GameObject> typePanel = new Dictionary<Type, GameObject>();

  
    //List<GameObject, GameObject>  
    // Start is called before the first frame update
    void Start()
    {
        string className = StartClass;

        // Load the assembly containing the class
        Assembly assembly = typeof(TestReflection).Assembly; // or Assembly.LoadFrom("path/to/YourAssembly.dll")

        // Get the type information of the start class
        Type classType = assembly.GetType(className);
        classAssociated.Add(classType);
        while (classAssociated.Count > 0)
        {
            Type ty = classAssociated[0];
          
            if (!typePanel.ContainsKey(ty))
            {
                //Parse the class details and get the Class object;
                Class classIns = ParseClass.getClassFromType(ty);
                if (classIns != null)
                {
                    
                    classAssociated.Remove(ty);

                    //Get all the associated, inherited and implemented classes.
                    if(!association.ContainsKey(ty))
                    association.Add(ty, new List<Type>());
                    association[ty].AddRange(classIns.associated);
                    classAssociated.AddRange(classIns.associated);

                    if(!inherited.ContainsKey(ty))
                    inherited.Add(ty, new List<Type>());
                    inherited[ty].AddRange(classIns.inherited);
                    classAssociated.AddRange(classIns.inherited);

                    if(!implemented.ContainsKey(ty))
                    implemented.Add(ty, new List<Type>());
                    implemented[ty].AddRange(classIns.implemented);
                    classAssociated.AddRange(classIns.implemented);

                    //Use the class obj to generate the diagram of the class (UI panel)
                    GameObject panel = classDiagram.CreateClassDiagram(classIns);
                    if(!typePanel.ContainsKey(ty))
                    typePanel.Add(classIns.type, panel);
                    panel.GetComponent<DraggablePanel>().canvas = canvasObj;
                    panels.Add(panel);
                }

            }
            classAssociated.Remove(ty);
        }

         //HACK : invoking this after a small delay as this will give time for us to access the panel rect values properly.
         Invoke("SpaceOutClassPanels", 0.01f);
        
    }



    float right = 0;
    float bottom = 0;
    float offset = 50;
    //This is the space out the individual class diagrams - not overlapping. Like a ladder growing in top right direction.
    //TODO : Write a better algorithm to spread out the panel, like in a circle. 
    void SpaceOutClassPanels()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            Vector2 anchoredPosition = panels[i].GetComponent<RectTransform>().anchoredPosition;
    
            anchoredPosition.x =  right + offset;
            anchoredPosition.y = bottom + offset;
            // anchoredPosition.y = ;
            panels[i].GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            right += panels[i].GetComponent<RectTransform>().rect.width;
            bottom += panels[i].GetComponent<RectTransform>().rect.height/ 2;
       
        }


       //This is where to link all the relations between the classes
       AddRelationalLinks();

    }

    //Creates links between the class panels and links them using a line renderer
    void AddRelationalLinks()
    {
        //Debug.Log("addlinks " + typePanel.Count  + " : "+ typePanel.ToString());
        foreach(Type type in association.Keys)
        {
            List <Type> tolist = association[type];
            foreach(Type to in tolist)
            {
                if(!typePanel.ContainsKey(to) || !typePanel.ContainsKey(type))
                {
                    continue;
                }
                GameObject link = Instantiate(associationPrefab);
                AssociationLine assline = link.GetComponent<AssociationLine>();
                assline.panel = typePanel.ContainsKey(type) ? typePanel[type] : null;
                assline.panel2 = typePanel.ContainsKey(to) ? typePanel[to] : null;
                assline.LinkSprite = null;
                assline.Initialize();
            }

        }

        foreach (Type type in inherited.Keys)
        {
            List<Type> tolist = inherited[type];
            foreach (Type to in tolist)
            {
                if (!typePanel.ContainsKey(to) || !typePanel.ContainsKey(type))
                {
                    continue;
                }
                GameObject link = Instantiate(associationPrefab);
                AssociationLine assline = link.GetComponent<AssociationLine>();
                assline.panel = typePanel.ContainsKey(type) ? typePanel[type] : null;
                assline.panel2 =  typePanel.ContainsKey(to) ? typePanel[to] : null ;
                assline.LinkSprite = inheritedSprite;
                assline.Initialize();
            }

        }


        foreach (Type type in implemented.Keys)
        {
            List<Type> tolist = implemented[type];
            foreach (Type to in tolist)
            {
                if (!typePanel.ContainsKey(to) || !typePanel.ContainsKey(type))
                {
                    continue;
                }
                GameObject link = Instantiate(associationPrefab);
                AssociationLine assline = link.GetComponent<AssociationLine>();
                assline.panel = typePanel.ContainsKey(type) ? typePanel[type] : null;
                assline.panel2 = typePanel.ContainsKey(to) ? typePanel[to] : null;
                assline.LinkSprite = implementedSprite;
                assline.Initialize();
            }

        }
    }
}