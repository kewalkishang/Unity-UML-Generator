using UnityEngine;
using UnityEngine.EventSystems;

//Class to enable dragging of the class panels
public class DraggablePanel : MonoBehaviour,  IDragHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
