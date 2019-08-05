using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck {

    public static CollisionInfo CheckCollision(Vector3 point1, Vector3 point2, float rad1, float rad2)
    {
        Vector3 dir = (point2 - point1).normalized;

        if (((point2 + rad2 * dir * -1) - point1).magnitude < rad1)
        {
            return new CollisionInfo
            {
                hit = point2 + rad2 * dir * -1,
                isCollide = true
            };
        }


        return new CollisionInfo { isCollide = false };
    }

    //public static CollisionInfo CheckCollision()

    public static CollisionInfo CheckCollision(Rect rect1, Rect rect2)
    {
        Vector3 dir = (rect2.center - rect1.center).normalized;

        bool overlap = rect2.Overlaps(rect1);

        if (overlap)
        {
            return new CollisionInfo
            {
                hit = GetNearestPoint(rect1, rect2),
                isCollide = overlap,
            };
        }

        return new CollisionInfo
        {
            isCollide = false
        };
    }

    public static Vector3 GetNearestPoint(Rect rect1, Rect rect2)
    {
        Vector3 dir = (rect2.center - rect1.center).normalized;
        Vector3 nearestPoint = Vector3.zero;

        if (dir.x > 0)
            nearestPoint.x = rect2.min.x;
        if (dir.x < 0)
            nearestPoint.x = rect2.max.x;

        if (dir.y > 0)
            nearestPoint.y = rect2.min.y;
        if (dir.y < 0)
            nearestPoint.y = rect2.max.y;

        float distanceX = (rect2.center.x - rect1.center.x);
        float distanceY = (rect2.center.y - rect1.center.y);

        if (distanceX < rect2.width)
            nearestPoint.x -= (rect2.width - distanceX);

        if (distanceY < rect2.height)
            nearestPoint.y -= (rect2.height - distanceY);

        return nearestPoint;
        
    }


    /// <summary>
    /// Get CollisionInfo using 1 dimension axis
    /// </summary>
    /// <param name="x1">Object 1 position</param>
    /// <param name="x2">Object 2 position</param>
    /// <param name="d1">Max distance from object 1</param>
    /// <param name="d2">Width of object 2</param>
    /// <returns></returns>
    public static CollisionInfo1D CheckCollision1D(float x1, float x2, float d1, float d2)
    {
        float dir = Mathf.Sign(x2 - x1);
        Debug.Log(string.Format("Distance {0}", (x2 + d2 * dir * -1) - x1));

        if (Mathf.Abs((x2 + d2 * dir * -1) - x1) < d1)
        {
            return new CollisionInfo1D
            {
                hit = x2 + d2 * dir * -1,
                isCollide = true
            };
        }

        return new CollisionInfo1D { isCollide = false };
    }

    public static CollisionInfo1D CheckCollision1D(float x1, float x2, float d1, float d2, float dir)
    {
        float direction = Mathf.Sign(x2 - x1);

        if (dir * direction > 0)
            return CheckCollision1D(x1, x2, d1, d2);
        else
            return new CollisionInfo1D { isCollide = false };
    }

    public static float GetDistance(Vector3 point1, Vector3 point2)
    {
        return (point2 - point1).magnitude;
    }

    public static Vector3 GetDirection(Vector3 point1, Vector3 point2)
    {
        return (point2 - point1).normalized;
    }
}

public class CollisionInfo
{
    public Vector3 hit;
    public bool isCollide;
}

public class CollisionInfo1D
{
    public float hit;
    public bool isCollide;
}