using UnityEngine;

public class HelperUtils
{
    public Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-50f, 50f);
        float z = Random.Range(-50f, 50f);
        return new Vector3(x, 0, z);
    }
}