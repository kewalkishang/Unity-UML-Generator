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

        CreateDiagram2(classInst);

    }

    void CreateDiagram(Class classIns)
    {
        //Render Panel
        GameObject panel = Instantiate(PanelPrefab, Vector3.zero, Quaternion.identity);
        panel.transform.SetParent(Canvas.transform);

        panel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        Transform content = panel.transform;


        //Add Class Name;
        GameObject nameText = Instantiate(namePrefab, Vector3.zero, Quaternion.identity);
        nameText.transform.SetParent(content);
        nameText.GetComponent<TMP_Text>().text = classIns.name;
        //Position name;

        //panel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 400);

        GameObject divider = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        divider.transform.SetParent(content);

        //TextGenerator textGenerator = new TextGenerator();

        Attribute[] attributes = classIns.attributes;
        for(int i = 0; i < attributes.Length; i++)
        {
            GameObject attributeText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            attributeText.transform.SetParent(content);
            attributeText.GetComponent<TMP_Text>().text = attributes[i].ToUMLStandard();
           // maxWidth = Mathf.Max(maxWidth, attributeText.GetComponent<TMP_Text>().preferredWidth);

        }

        GameObject dividera = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        dividera.transform.SetParent(content);


        Operation[] operations = classIns.operations;
        for (int i = 0; i < operations.Length; i++)
        {
            GameObject operationText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            operationText.transform.SetParent(content);
            operationText.GetComponent<TMP_Text>().text = operations[i].ToUMLStandard();
           // maxWidth = Mathf.Max(maxWidth, operationText.GetComponent<TMP_Text>().preferredWidth);

        }
        //Debug.Log("Width "+maxWidth);
        //float wid = panel.GetComponent<RectTransform>().sizeDelta.x;
        //Debug.Log("Width " + wid);
        Vector2 newPosition = nameText.GetComponent<RectTransform>().anchoredPosition;
        //Debug.Log("Pos " + newPosition.x);
        //Debug.Log("Posadsa " + nameText.GetComponent<RectTransform>().anchoredPosition3D.x);
        newPosition.x +=  400;
        nameText.GetComponent<RectTransform>().anchoredPosition = newPosition;
        //Change width of name , divider
        // GameObject viewPort = panel.GetComponentInChildren<View>().gameObject
        // panel.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, 400);
        //divider.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, 10);
        //dividera.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, 10);

       
    }


    void CreateDiagram2(Class classIns)
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
        //Position name;

        GameObject panel = Instantiate(PanelPrefab, Vector3.zero, Quaternion.identity);
        panel.transform.SetParent(content);
        Transform panelcontent = panel.transform;
        //panel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 400);

        GameObject divider = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        divider.transform.SetParent(panelcontent);

        //TextGenerator textGenerator = new TextGenerator();

        Attribute[] attributes = classIns.attributes;
        for (int i = 0; i < attributes.Length; i++)
        {
            GameObject attributeText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            attributeText.transform.SetParent(panelcontent);
            attributeText.GetComponent<TMP_Text>().text = attributes[i].ToUMLStandard();
            // maxWidth = Mathf.Max(maxWidth, attributeText.GetComponent<TMP_Text>().preferredWidth);

        }

        GameObject dividera = Instantiate(dividerLinePrefab, Vector3.zero, Quaternion.identity);
        dividera.transform.SetParent(panelcontent);


        Operation[] operations = classIns.operations;
        for (int i = 0; i < operations.Length; i++)
        {
            GameObject operationText = Instantiate(AttributePrefab, Vector3.zero, Quaternion.identity);
            operationText.transform.SetParent(panelcontent);
            operationText.GetComponent<TMP_Text>().text = operations[i].ToUMLStandard();
            // maxWidth = Mathf.Max(maxWidth, operationText.GetComponent<TMP_Text>().preferredWidth);

        }
        //Debug.Log("Width "+maxWidth);
        //float wid = panel.GetComponent<RectTransform>().sizeDelta.x;
        //Debug.Log("Width " + wid);
       // Vector2 newPosition = nameText.GetComponent<RectTransform>().anchoredPosition;
        //Debug.Log("Pos " + newPosition.x);
        //Debug.Log("Posadsa " + nameText.GetComponent<RectTransform>().anchoredPosition3D.x);
       // newPosition.x += 400;
        //nameText.GetComponent<RectTransform>().anchoredPosition = newPosition;
        //Change width of name , divider
        // GameObject viewPort = panel.GetComponentInChildren<View>().gameObject
        // panel.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, 400);
        //divider.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, 10);
        //dividera.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, 10);


    }
  

}
