using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class VariableScrollRect : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab = default;
    
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _content;

    public GameObject SpawnItem(GameObject item)
    {
        if(_content == null)
            _content = _scrollRect.content;

        GameObject instance = Instantiate(item, _scrollRect.content.transform);

        if(_content.TryGetComponent<GridLayoutGroup>(out var grid))
        {
            int colCount = grid.constraintCount;
            int rowCount = _scrollRect.content.childCount / colCount;
            float Yoffset = grid.cellSize.y + grid.spacing.y*2;
            _content.sizeDelta = new Vector2(_scrollRect.content.sizeDelta.x, rowCount * Yoffset);
        }

        return instance;
    }
    
    [ContextMenu("SpawnItem")]
    public void SpawnItem()
    {
        SpawnItem(itemPrefab);
    }
}