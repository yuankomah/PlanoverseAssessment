using System;
using UnityEngine.InputSystem;
using UnityEngine;

public static class Collision
{
    public const int DEFAULT_LAYER = 1;
    public const string FLOOR = "Floor";
    public const float RAY_MAXIMUM_RANGE = 100f;
    public const float BASE_POSITION_Y = 0f;

    public static bool CollideWithComponent(Vector3 placementPosition)
    {
        float radius = 0.5f;
        int totalCollision = 0, minimumCollisionNeeded = 2;
        Collider[] colliders = Physics.OverlapSphere(placementPosition, radius);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.name != FLOOR) totalCollision++;
        }

        return totalCollision >= minimumCollisionNeeded;
    }
}
