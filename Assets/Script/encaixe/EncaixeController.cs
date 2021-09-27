using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class EncaixeController : MonoBehaviour
{
    [SerializeField] private EncaixeData encaixeData;

    [Header("Prefabs")]
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject pieceSlotPrefab;
    
    [Header("References")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform piecesContainer;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private FeedbackUI feedbackUI;

    private DraggablePiece[] _pieces;
    private PieceSlot[] _pieceSlots;

    private int _numSnappedPieces = 0;
    private int _loadedImgs = 0;

    public int right = 0;
    public int wrong = 0;

    private void Awake()
    {
        SetCurrentEncaixe();
        SpawnPieces();
    }

    private void SetCurrentEncaixe()
    {
        if (CurrentGameSession.TryGetGameOf("encaixe", out EncaixeData gameData))
        {
            encaixeData = gameData;
        }
    }

    private void PieceSnapped()
    {
        _numSnappedPieces += 1;

        if(_numSnappedPieces >= _pieces.Length)
        {
            feedbackUI.Open();
        }
    }

    [ContextMenu("Spawn Pieces")]
    private void SpawnPieces()
    {
        loadingScreen.SetActive(true);

        var numPieces = encaixeData.pieces.Length;
        var piecesInfo = encaixeData.pieces;
        _pieces = new DraggablePiece[numPieces];
        _pieceSlots = new PieceSlot[numPieces];

        Vector2[] _slotsPosition = new Vector2[numPieces];

        for(int i = 0; i < numPieces; i++)
        {
            _slotsPosition[i].x = -1;
            _slotsPosition[i].y = -1;
        }

        for (int i = 0; i < numPieces; i++)
        {
            GameObject instantiatedPieceSlot = Instantiate(pieceSlotPrefab, piecesContainer);

            //instantiatedPieceSlot.transform.position = piecesInfo[i].position * 1.5f;

            RectTransform rectTransform = instantiatedPieceSlot.GetComponent<RectTransform>();

            instantiatedPieceSlot.transform.position = new Vector2(Random.Range(rectTransform.rect.width/2, Screen.width - rectTransform.rect.width/2), Random.Range(rectTransform.rect.height/2, Screen.height - rectTransform.rect.height/2));
            
            for(int j = 0; j < numPieces; j++)
            {
                if(_slotsPosition[j] == new Vector2(-1, -1))
                {
                    _slotsPosition[j].x = instantiatedPieceSlot.transform.position.x;
                    _slotsPosition[j].y = instantiatedPieceSlot.transform.position.y;
                }
                else
                {
                    if(_slotsPosition[j].x == instantiatedPieceSlot.transform.position.x && _slotsPosition[j].y == instantiatedPieceSlot.transform.position.y)
                    {
                        Debug.Log("Igual");
                        instantiatedPieceSlot.transform.position = new Vector2(Random.Range(rectTransform.rect.width/2, Screen.width - rectTransform.rect.width/2), Random.Range(rectTransform.rect.height/2, Screen.height - rectTransform.rect.height/2));
                        j = 0;
                    }
                }
            }

            _pieceSlots[i] = instantiatedPieceSlot.GetComponent<PieceSlot>();
        }
        
        Vector2[] _piecesPosition = new Vector2[numPieces];

        for(int i = 0; i < numPieces; i++)
        {
            _piecesPosition[i].x = -1;
            _piecesPosition[i].y = -1;
        }

        for(int i = 0; i < numPieces; i++)
        {
            GameObject instantiatedPiece = Instantiate(piecePrefab, piecesContainer);

            instantiatedPiece.transform.position = piecesInfo[i].position;

            _pieces[i] = instantiatedPiece.GetComponent<DraggablePiece>();
            
            StartCoroutine(SetPieceSprite(piecesInfo[i], i));
        }

        _numSnappedPieces = 0;
        _loadedImgs = 0;
    }

    private IEnumerator SetPieceSprite(EncaixeData.Piece pieceInfo, int index)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(pieceInfo.imageURL))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);

                _pieces[index].Setup(index, texture, PieceSnapped);
                _pieceSlots[index].Setup(index, texture);
            }

            _loadedImgs++;
            if(_loadedImgs >= _pieceSlots.Length)
            {
                loadingScreen.SetActive(false);
            }
        }
    }
}
