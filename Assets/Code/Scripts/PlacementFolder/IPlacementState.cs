using UnityEngine;

public interface IPlacementState
{
    void EndState();
    void OnAction(Vector3Int gridPos);
    void UpdateState(Vector3Int gridPos);
}