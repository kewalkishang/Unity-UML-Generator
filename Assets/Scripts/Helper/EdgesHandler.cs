using UnityEngine;

//Takes care of Finding the edges of the panels. 
public class EdgesHandler : MonoBehaviour
{

    //public GameObject panel;
    public RectTransform rectTransform;
    public GameObject imageRight;
    public GameObject imageLeft;
    public GameObject imageTop;
    public GameObject imageBottom;


    // Start is called before the first frame update
    void Start()
    {
        //HACK : Adding a delay to make sure we have the right edge sizes.
        Invoke("SetEdges", 1f);
        
    }

    void SetEdges()
    {
        float PosLeft = rectTransform.rect.xMin;
        float PosRight = rectTransform.rect.xMax;
        float PosTop = rectTransform.rect.yMin;
        float PosBottom = rectTransform.rect.yMax;

        //Update image positions.
        imageLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(PosLeft, 0);
        imageRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(PosRight, 0);
        imageTop.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, PosTop);
        imageBottom.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, PosBottom);
    }
  
}
