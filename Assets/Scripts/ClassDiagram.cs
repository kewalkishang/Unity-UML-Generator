using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

        Class classInst = new Class("DemoClass", attributes, operations);

        //CreateDiagram(classInst);
        CreateNewDiagram(classInst);
    }

    

    public GameObject CreateDiagram(Class classIns)
    {
        //Render Panel
        GameObject mainpanel = Instantiate(MainPanelPrefab, Vector3.zero, Quaternion.identity);
        mainpanel.transform.SetParent(Canvas.transform);

        mainpanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        
        Transform content = mainpanel.transform;


        //Add Class Name;
        GameObject nameText = Instantiate(namePrefab, Vector3.zero, Quaternion.identity);
        nameText.transform.SetParent(content);
        nameText.GetComponent<TMP_Text>().text = classIns.name;
        mainpanel.name = classIns.name;
        //Position name;

        GameObject panel = Instantiate(PanelPrefab, Vector3.zero, Quaternion.identity);
        panel.transform.SetParent(content);
        Transform panelcontent = panel.transform;

        GameObject divider = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        divider.transform.SetParent(panelcontent);


        Attribute[] attributes = classIns.attributes;
        for (int i = 0; i < attributes.Length; i++)
        {
            GameObject attributeText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            attributeText.transform.SetParent(panelcontent);
            attributeText.GetComponent<TMP_Text>().text = attributes[i].ToUMLStandard();

        }

        GameObject dividera = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        dividera.transform.SetParent(panelcontent);


        Operation[] operations = classIns.operations;
        for (int i = 0; i < operations.Length; i++)
        {
            GameObject operationText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            operationText.transform.SetParent(panelcontent);
            operationText.GetComponent<TMP_Text>().text = operations[i].ToUMLStandard();

        }
       // LayoutRebuilder.ForceRebuildLayoutImmediate(mainpanel.GetComponent<RectTransform>());
       // Vector3[] panelCorners = new Vector3[4];
       // mainpanel.GetComponent<RectTransform>().GetWorldCorners(panelCorners);
      //  Debug.Log("Main panel " + mainpanel.name + " : " + panelCorners[0].x + " : " + panelCorners[0].y + " : " + panelCorners[2].x + " : " + panelCorners[2].y + " : " + (panelCorners[2].x - panelCorners[0].x) + " : " + (panelCorners[2].y - panelCorners[0].y) + ":" +  mainpanel.GetComponent<RectTransform>().sizeDelta.x) ;
        // Rect rect = mainpanel.GetComponent<RectTransform>().rect;
        //Debug.Log("Main panel " + rect.x + " : " + rect.y + " : " + rect.center + " : " + rect.left + " : " + rect.right + " : " + rect.top + " : " + rect.bottom);

        //Vector2 pos = mainpanel.GetComponent<RectTransform>().anchoredPosition;
        return mainpanel;
       // Debug.Log("Vex panel " + pos.x + " : " + pos.y );

    }

    public GameObject CreateNewDiagram(Class classIns)
    {
        GameObject maingameobject = Instantiate(NewEmpty, Vector3.zero, Quaternion.identity);
        maingameobject.transform.SetParent(Canvas.transform);
        maingameobject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        GameObject outer = maingameobject.transform.Find("MainPanel Variant").gameObject;
        GameObject cname = outer.transform.Find("ClassName").gameObject;

        //Assign class name.
        cname.GetComponent<TMP_Text>().text = classIns.name;

        GameObject panel = outer.transform.Find("Panel").gameObject;
        Transform panelcontent = panel.transform;

        GameObject divider = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        divider.transform.SetParent(panelcontent);


        Attribute[] attributes = classIns.attributes;
        for (int i = 0; i < attributes.Length; i++)
        {
            GameObject attributeText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            attributeText.transform.SetParent(panelcontent);
            attributeText.GetComponent<TMP_Text>().text = attributes[i].ToUMLStandard();

        }

        GameObject dividera = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        dividera.transform.SetParent(panelcontent);


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
