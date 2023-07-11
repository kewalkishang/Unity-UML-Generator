using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPanels : MonoBehaviour
{

    LineRenderer lineRenderer;
   // GameObject pivot;

    public GameObject panel;
    RectTransform rectTransform;
    public GameObject image;

    public GameObject panel2;
    RectTransform rectTransform2;
    public GameObject image2;
    public GameObject line;
    private void Start()
    {

        GameObject lr = Instantiate(line, Vector3.zero, Quaternion.identity);
        lineRenderer = lr.gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        //pivot = Instantiate(pre);
        rectTransform = panel.GetComponent<RectTransform>();
        rectTransform2 = panel2.GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Vector3 panelPosition = startPoint.position;
        //Vector3 panelScale = startPoint.localScale;


        //prefab.transform.position = endPoint.position - Vector3.right * (endPoint.localScale.x / 2f);


        //Vector3 panel2Position = prefab.transform.position;
        //Vector3 panel2Scale = prefab.transform.localScale;
        ////// Calculate the positions of the panel's sides
        //Vector3 leftSide = panelPosition - Vector3.right * (panelScale.x / 2f);
        //Vector3 rightSide = panel2Position + Vector3.right * (panel2Scale.x / 2f);

        //// lineRenderer.SetPosition(0, startPoint.position);
        ////lineRenderer.SetPosition(1, endPoint.position);
        //lineRenderer.SetPosition(0, leftSide);
        //lineRenderer.SetPosition(1, prefab.transform.position);


        float p1PosLeft = rectTransform.rect.xMin;
        float p1PosRight = rectTransform.rect.xMax;

        float p1PosTop = rectTransform.rect.yMin;
        float p1PosBottom = rectTransform.rect.yMax;
        // Debug.Log("LEFT " + left);
        float p2PosLeft = rectTransform2.rect.xMin;
        float p2PosRight = rectTransform2.rect.xMax;

        float p2PosTop = rectTransform2.rect.yMin;
        float p2PosBottom = rectTransform2.rect.yMax;

        Vector2 pos1 = rectTransform.anchoredPosition;
        Vector2 pos2 = rectTransform2.anchoredPosition;
        Debug.Log("P1 rect" + p1PosLeft + " : " + p1PosRight);
        Debug.Log("P2 rect" + p2PosLeft + " : " + p2PosRight);
        Debug.Log("P1 " + pos1.x + " : " + pos1.y + " : "  + rectTransform.rect.width);
        Debug.Log("P2 " + pos2.x + " : " + pos2.y + " : " + rectTransform2.rect.width);
        float p1Pos = 0;
        float p2Pos = 0;
        float p1Posy = 0;
        float p2Posy = 0;
        
       
        if (pos1.y > pos2.y)
        {
            //p1Pos = 0;
            //p2Pos = 0;
            p1Posy = p1PosTop;
            p2Posy = p2PosBottom;
        }
        else
        if(pos1.y < pos2.y)
        {
            //p1Pos = 0;
            //p2Pos = 0; 
            p1Posy = p1PosBottom;
            p2Posy = p2PosTop;

        }
        if (pos1.x < pos2.x)
        {

            p1Pos = p1PosRight;
            p2Pos = p2PosLeft;
            //p1Posy = 0;
            //p2Posy = 0;

        }
        else if (pos1.x > pos2.x)
        {
            p1Pos = p1PosLeft;
            p2Pos = p2PosRight;
            //p1Posy = 0;
            //p2Posy = 0;
        }

       // float right = rectTransform2.rect.xMin;
        Debug.Log("Pos 1 " + p1Pos + " : " + p1Posy + " : " + p2Pos + " : " + p2Posy);

        //Update image position.
        image.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150, p1Posy);
        image2.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, p2Posy);

        //Connect the 2 images
        lineRenderer.SetPosition(0, image.transform.position);
        lineRenderer.SetPosition(1, image2.transform.position);



    }
}
