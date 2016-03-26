using UnityEngine;
using System.Collections;


public class NumberString {

    static string[] incPrefixes = { "K", "M", "G", "T", "P", "E", "Z", "Y", "KY", "MY", "GY", "TY" };

    public static string ToSI(float d)
    {
        if (d < 1000)
            return d + "";

        float log10 = Mathf.Log10(d);
        int degree = (int)(Mathf.Log10(d)/3);
        float scaled = d * Mathf.Pow(1000, -degree);
        
        return System.String.Format("{0:###.0}" + incPrefixes[degree - 1], scaled);
    }

}
