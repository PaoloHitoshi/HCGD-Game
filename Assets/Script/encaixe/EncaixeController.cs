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

        for (int i = 0; i < numPieces; i++)
        {
            GameObject instantiatedPieceSlot = Instantiate(pieceSlotPrefab, piecesContainer);

            instantiatedPieceSlot.transform.position = piecesInfo[i].position * 1.2f;
            
            _pieceSlots[i] = instantiatedPieceSlot.GetComponent<PieceSlot>();
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
