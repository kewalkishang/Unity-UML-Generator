using UnityEngine;
using TMPro;

//This class takes care of drawing the UI panels which represents the class diagram.
public class ClassDiagram : MonoBehaviour
{
    public GameObject dividerLinePrefab;
    public GameObject PanelPrefab;
    public GameObject MainPanelPrefab;
    public GameObject namePrefab;
    public GameObject AttributePrefab;
    public GameObject Canvas;
    public GameObject NewEmpty;

    // Start is called before the first frame update
    void Start()
    {

        //For testing.
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

        Class classInst = new Class(typeof(Class),"DemoClass", attributes, operations);
        //CreateClassDiagram(classInst);
    }


    //This function returns the class diagram
    public GameObject CreateClassDiagram(Class classIns)
    {
        GameObject maingameobject = Instantiate(NewEmpty, Vector3.zero, Quaternion.identity);
        maingameobject.name = classIns.name;
        maingameobject.transform.SetParent(Canvas.transform);
        maingameobject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        GameObject outer = maingameobject.transform.Find("MainPanel Variant").gameObject;
        GameObject cname = outer.transform.Find("ClassName").gameObject;

        //Assign class name.
        cname.GetComponent<TMP_Text>().text = classIns.name;

        GameObject panel = outer.transform.Find("Panel").gameObject;
        Transform panelcontent = panel.transform;

        //A Divider Line -Divide the name and attributes
        GameObject divider = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        divider.transform.SetParent(panelcontent);

        //Add all the Attributes
        Attribute[] attributes = classIns.attributes;
        for (int i = 0; i < attributes.Length; i++)
        {
            GameObject attributeText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            attributeText.transform.SetParent(panelcontent);
            attributeText.GetComponent<TMP_Text>().text = attributes[i].ToUMLStandard();

        }


        //A Divider Line - Divide the attributes and methods
        GameObject dividera = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        dividera.transform.SetParent(panelcontent);

        //Add all the Operations/Methods
        Operation[] operations = classIns.operations;
        for (int i = 0; i < operations.Length; i++)
        {
            GameObject operationText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            operationText.transform.SetParent(panelcontent);
            operationText.GetComponent<TMP_Text>().text = operations[i].ToUMLStandard();

        }


        return maingameobject;
    }
  

}
