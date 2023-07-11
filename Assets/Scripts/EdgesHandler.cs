using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgesHandler : MonoBehaviour
{

    //public GameObject panel;
    public RectTransform rectTransform;
    public GameObject imageRight;
    public GameObject imageLeft;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetEdges", 1f);
        
    }

    void SetEdges()
    {
        float p1PosLeft = rectTransform.rect.xMin;
        float p1PosRight = rectTransform.rect.xMax;

        //Update image position.
        imageLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1PosLeft, 0);
        imageRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1PosRight, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //float p1PosLeft = rectTransform.rect.xMin;
        //float p1PosRight = rectTransform.rect.xMax;

        ////Update image position.
        //imageLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1PosLeft, 0);
        //imageRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1PosRight, 0);

    }
}
