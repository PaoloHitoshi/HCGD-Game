using UnityEngine;
/// <summary>
/// Implement this interface to be dropable
/// </summary>
public interface IDropable
{
    void OnDrop(Vector3 position);
}