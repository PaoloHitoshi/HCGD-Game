using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class VariableScrollRect : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab = default;
    
    private ScrollRect _scrollRect;
    private RectTransform _content;


    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _content = _scrollRect.content;
    }

    public void SpawnItem(GameObject item)
    {

        Instantiate(item, _scrollRect.content.transform);

        if(_content.TryGetComponent<GridLayoutGroup>(out var grid))
        {
            int colCount = grid.constraintCount;
            int rowCount = _scrollRect.content.childCount / colCount;
            float Yoffset = grid.cellSize.y + grid.spacing.y*2;
            _content.sizeDelta = new Vector2(_scrollRect.content.sizeDelta.x, rowCount * Yoffset);
        }

    }
    
    [ContextMenu("SpawnItem")]
    public void SpawnItem()
    {
        SpawnItem(itemPrefab);
    }
}