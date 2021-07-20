using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropable
{
    public int Id { get; private set; } = -1;

    [SerializeField] private RawImage image;

    private Vector3 _initialPosition;
    private RectTransform _rectTransform;
    private Action _onDrop;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Setup(int index, Texture2D texture, Action onDrop)
    {
        Setup(index, texture);
        _onDrop = onDrop;
    }

    public void Setup(int index, Texture2D texture)
    {
        Id = index;
        image.texture = texture;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        _initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {   
        _rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.position = _initialPosition;
    }

    public void OnDrop(Vector3 position)
    {
        transform.position = position;
        _onDrop?.Invoke();
    }
}