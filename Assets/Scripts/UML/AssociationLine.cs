using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class to draw the relationship lines between 2 class panels.
//Also handle changing the link directions and associated class edge so that user can move it around. 
public class AssociationLine : MonoBehaviour
{
    public Sprite LinkSprite;

    LineRenderer lineRenderer;

    public GameObject panel;
    GameObject image;

    public GameObject panel2;
    GameObject image2;


    public GameObject line;
    public GameObject img;
    bool started = false;

    float leftRot = 90f;
    float rightRot = -90f;
    float botRot = 0f;
    float topRot = 180f;

    RectTransform anchorTransform;
    RectTransform anchorTransform2;

    //Used to change link from vertical to horizontal
    //HACK : Just a magic number based on avg width of panels. 
    public float verticalOffset = 200f;


    Transform Panel1leftEdge;
    Transform Panel1rightEdge;
    Transform Panel1topEdge;
    Transform Panel1bottomEdge;

    Transform Panel2leftEdge;
    Transform Panel2rightEdge;
    Transform Panel2topEdge;
    Transform Panel2bottomEdge;

    // Start is called before the first frame update
    void Start()
    {    
       // Initialize();

    }

    //Creates the images that attaches to the edges of the panels.
    //Initializes the line renderer.
    public void Initialize()
    {

        if(panel == null || panel2 == null)
        {
            return;
        }

        Panel1leftEdge = panel.transform.Find("Left").transform;
        Panel1rightEdge = panel.transform.Find("Right").transform;
        Panel1topEdge = panel.transform.Find("Top").transform;
        Panel1bottomEdge = panel.transform.Find("Bottom").transform;

        Panel2leftEdge = panel2.transform.Find("Left").transform;
        Panel2rightEdge = panel2.transform.Find("Right").transform;
        Panel2topEdge = panel2.transform.Find("Top").transform;
        Panel2bottomEdge = panel2.transform.Find("Bottom").transform;

        image = Instantiate(img, Vector3.zero, Quaternion.identity);
        image.transform.SetParent(Panel1leftEdge);
        //image.GetComponent<Image>().sprite = LinkSprite;

        image2 = Instantiate(img, Vector3.zero, Quaternion.identity);
        image2.transform.SetParent(Panel2rightEdge);

        image2.GetComponent<Image>().sprite = LinkSprite;
        image2.GetComponent<Image>().color = LinkSprite == null ? new Color(1, 1, 1, 0) :  new Color(1, 1, 1, 255);
        

        GameObject lr = Instantiate(line, Vector3.zero, Quaternion.identity);
        lineRenderer = lr.gameObject.GetComponent<LineRenderer>();
        lineRenderer.material.SetColor("_Color", Color.gray);
        lineRenderer.positionCount = 2;
       
        anchorTransform = panel.GetComponent<RectTransform>();
        anchorTransform2 = panel2.GetComponent<RectTransform>();

        //Prevent crash - ensure everthing is initialized before we use the link edge change logic
        started = true;
    }

    // Update is called once per frame
    //The logic is simple - We shift the parent of the image between 4 options.
    //Top, Bottom, Left, Right - Which is taken care in EdgesHandler class.
    //Based on the anchored position of the 2 class panels.
    //TODO : Move this into draggable class so this only needs to taken care when a class is moved.
    void Update()
    {

        if (started)
        {
         
            Vector2 pos1 = anchorTransform.anchoredPosition;
            Vector2 pos2 = anchorTransform2.anchoredPosition;

          
             if (Mathf.Abs(pos1.x - pos2.x) >= verticalOffset)
                {
                   
         

                if (pos1.x > pos2.x)
                {
                    image.transform.SetParent(Panel1leftEdge);
                    image.transform.rotation = Quaternion.Euler(0, 0, rightRot);
                    image2.transform.SetParent(Panel2rightEdge);
                    image2.transform.rotation = Quaternion.Euler(0, 0, leftRot);
                    }
                    else
                    {
                        image.transform.SetParent(Panel1rightEdge);
                        image.transform.rotation = Quaternion.Euler(0, 0, leftRot);
                        image2.transform.SetParent(Panel2leftEdge);
                        image2.transform.rotation = Quaternion.Euler(0, 0, rightRot);
                  }

            }
            else
            {
                if (pos1.y > pos2.y)
                {
                    image.transform.SetParent(Panel1topEdge);
                    image.transform.rotation = Quaternion.Euler(0, 0, botRot);
                    image2.transform.SetParent(Panel2bottomEdge);
                    image2.transform.rotation = Quaternion.Euler(0, 0, topRot);

                }
                else
                if (pos1.y < pos2.y)
                {
                    image2.transform.SetParent(Panel2topEdge);
                    image2.transform.rotation = Quaternion.Euler(0, 0, botRot);
                    image.transform.SetParent(Panel1bottomEdge);
                    image.transform.rotation = Quaternion.Euler(0, 0, topRot);
                }


            }


            //Connect the 2 images. and readjust the linerenderer as the panel moves around 
            lineRenderer.SetPosition(0, image.transform.position);
            lineRenderer.SetPosition(1, image2.transform.position);
        }
    }
}
