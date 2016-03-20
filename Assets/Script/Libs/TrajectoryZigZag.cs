using UnityEngine;
using System.Collections;


public class TrajectoryZigZag
{
    static Vector3 Noise = new Vector3(2f, 0f, 2f);
    public static Vector3 Update(Vector3 start, Vector3 end, float t)
    {
        Vector3 pos = Vector3.MoveTowards(start, end, t);
        pos += new Vector3(Random.Range(-Noise.x, Noise.x), Random.Range(-Noise.y, Noise.y), Random.Range(-Noise.z, Noise.z)) * t;
        return pos;
    }
}
