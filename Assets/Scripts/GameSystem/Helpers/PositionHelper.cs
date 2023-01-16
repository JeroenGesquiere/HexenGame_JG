using BoardSystem;
using UnityEngine;


public class PositionHelper : MonoBehaviour
{
    public const int TileSize = 1;

    public static Position GridPosition(Vector3 worldPosition)
    {

        Vector3 scaleWorldPosition = worldPosition / TileSize;
        int gridPostionR = Mathf.RoundToInt(scaleWorldPosition.z * 0.65f);

        int gridPositionColom = Mathf.RoundToInt((scaleWorldPosition.x - (Mathf.Abs(gridPostionR % 2) * (Mathf.Sqrt(3) / 2))) / Mathf.Sqrt(3));
        int gridPositionQ = gridPositionColom - (gridPostionR - (gridPostionR & 1)) / 2;


        return new Position(gridPositionQ, gridPostionR);
    }

    public static Vector3 WorldPosition(Position gridPosition)
    {

        float worldPositionY = (gridPosition.R / 0.65f);

        float worldPositionColom = gridPosition.Q + (gridPosition.R - (gridPosition.R & 1)) / 2;

        float worldPositionX = worldPositionColom * Mathf.Sqrt(3) + ((Mathf.Abs(gridPosition.R % 2) * (Mathf.Sqrt(3) / 2)));

        return new Vector3(worldPositionX, 0, worldPositionY);
    }

}
