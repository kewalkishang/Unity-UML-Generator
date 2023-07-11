using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssociationLine : MonoBehaviour
{

    //public Sprite inheritedSprite;
    public Sprite LinkSprite;

    LineRenderer lineRenderer;
    // GameObject pivot;

    public GameObject panel;
    RectTransform rectTransform;
    GameObject image;

    public GameObject panel2;
    RectTransform rectTransform2;
    GameObject image2;


    public GameObject line;
    public GameObject img;
    bool started = false;

    float leftRot = 90f;
    float rightRot = -90f;

    RectTransform anchorTransform;
    RectTransform anchorTransform2;
    // Start is called before the first frame update
    void Start()
    {
     
       // Initialize();

    }

    Transform Panel1leftEdge;
    Transform Panel1rightEdge;
    Transform Panel2leftEdge;
    Transform Panel2rightEdge;


    public void Initialize()
    {
        
        Panel1leftEdge = panel.transform.Find("Left").transform;
        Panel1rightEdge = panel.transform.Find("Right").transform;

        Panel2leftEdge = panel2.transform.Find("Left").transform;
        Panel2rightEdge = panel2.transform.Find("Right").transform;

        image = Instantiate(img, Vector3.zero, Quaternion.identity);
        image.transform.SetParent(Panel1leftEdge);
        //image.GetComponent<Image>().sprite = LinkSprite;

        image2 = Instantiate(img, Vector3.zero, Quaternion.identity);
        image2.transform.SetParent(Panel2rightEdge);

        image2.GetComponent<Image>().color = new Color(1, 1, 1, 255);
        image2.GetComponent<Image>().sprite = LinkSprite;

        GameObject lr = Instantiate(line, Vector3.zero, Quaternion.identity);
        lineRenderer = lr.gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
       
        anchorTransform = panel.GetComponent<RectTransform>();
        anchorTransform2 = panel2.GetComponent<RectTransform>();
    
        started = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (started)
        {
         
            Vector2 pos1 = anchorTransform.anchoredPosition;
            Vector2 pos2 = anchorTransform2.anchoredPosition;

            if (pos1.x < pos2.x)
            {
                image.transform.SetParent(Panel1rightEdge);
                image.transform.rotation = Quaternion.Euler(0, 0, leftRot);
                image2.transform.SetParent(Panel2leftEdge);
                image2.transform.rotation = Quaternion.Euler(0, 0, rightRot);

            }
            else
            {
                image.transform.SetParent(Panel1leftEdge);
                image.transform.rotation = Quaternion.Euler( 0, 0, rightRot);
                image2.transform.SetParent(Panel2rightEdge);
                image2.transform.rotation = Quaternion.Euler(0, 0, leftRot);
            }
        

            //Connect the 2 images
            lineRenderer.SetPosition(0, image.transform.position);
            lineRenderer.SetPosition(1, image2.transform.position);
        }
    }
}
