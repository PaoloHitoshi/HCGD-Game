using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropable
{
    private Vector3 _initialPosition;

    private RectTransform _rectTransform;
    private Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {   
        _rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.position = _initialPosition;
    }

    public void OnDrop(Vector3 position)
    {
        transform.position = position;
    }
}