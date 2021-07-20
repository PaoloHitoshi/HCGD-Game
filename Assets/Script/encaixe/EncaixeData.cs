using UnityEngine;

[System.Serializable]
public class EncaixeData : GameDataContainer
{
    public Piece[] pieces;

    [System.Serializable]
    public struct Piece
    {
        public string imageURL;
        public string feedbackSoundURL;
        public Vector2 position;
    }
}