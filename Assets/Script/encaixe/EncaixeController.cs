using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class EncaixeController : MonoBehaviour
{
    [SerializeField] private EncaixeData encaixeData;
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject pieceSlotPrefab;
    [SerializeField] private Canvas canvas;

    private DraggablePiece[] pieces;
    private PieceSlot[] pieceSlots;

    private void Awake()
    {
        SetCurrentEncaixe();   
    }

    private void SetCurrentEncaixe()
    {
        if (CurrentGameSession.TryGetGameOf("encaixe", out EncaixeData gameData))
        {
            encaixeData = gameData;
        }
    }

    [ContextMenu("Spawn Pieces")]
    private void SpawnPieces()
    {
        var numPieces = encaixeData.pieces.Length;
        var piecesInfo = encaixeData.pieces;
        pieces = new DraggablePiece[numPieces];
        pieceSlots = new PieceSlot[numPieces];

        for (int i = 0; i < numPieces; i++)
        {
            GameObject instantiatedPieceSlot = Instantiate(pieceSlotPrefab, canvas.transform);

            instantiatedPieceSlot.transform.position = piecesInfo[i].position * 1.2f;
            
            pieceSlots[i] = instantiatedPieceSlot.GetComponent<PieceSlot>();
        }
        
        for(int i = 0; i < numPieces; i++)
        {
            GameObject instantiatedPiece = Instantiate(piecePrefab, canvas.transform);
            
            instantiatedPiece.transform.position = piecesInfo[i].position;
            pieces[i] = instantiatedPiece.GetComponent<DraggablePiece>();
            
            StartCoroutine(SetPieceSprite(piecesInfo[i], i));
        }
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

                pieces[index].Setup(index, texture);
                pieceSlots[index].Setup(index, texture);
            }
        }
    }
}
