using UnityEngine;
using System.Collections;


public class TrajectoryMeteo
{

    public static Vector3 Update(Vector3 cur, Vector3 end, float t)
    {
        return Vector3.MoveTowards(cur, end, t);
    }
}
