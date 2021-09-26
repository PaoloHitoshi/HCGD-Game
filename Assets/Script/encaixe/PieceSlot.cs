using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceSlot : MonoBehaviour, IDropHandler
{
    public int Id { get; set; } = -1;

    [SerializeField] private RawImage image;

    [SerializeField] private EncaixeController encaixe;

    private void Awake()
    {
        encaixe = FindObjectOfType<EncaixeController>();
    }

    public void Setup(int index, Texture2D texture)
    {
        Id = index;
        image.texture = texture;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;
        
        if (obj != null && obj.TryGetComponent(out DraggablePiece piece))
        {
            if (piece.Id != Id) 
            {
                encaixe.wrong++; 
                
                //Debug.Log(encaixe.wrong); 
                
                return;
            }

            encaixe.right++; 
            
            //Debug.Log(encaixe.right);

            piece.OnDrop(transform.position);
            image.color = Color.white;

            Destroy(obj);
        }
    }
}