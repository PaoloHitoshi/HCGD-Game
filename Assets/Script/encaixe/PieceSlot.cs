using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image _pieceMock;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;
        
        if (obj != null && obj.TryGetComponent(out IDropable piece))
        {
            piece.OnDrop(transform.position);
            _pieceMock.color = Color.white;
            
            Destroy(obj);
        }
    }
}