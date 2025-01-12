using UnityEngine;

public class SmoothCameraShake : MonoBehaviour
{
    public float duration = 0.1f;
    public float magnitude = 0.1f;

    private float elapsed = 0.0f;

    public void StartShake()
    {
        elapsed = duration;
    }

    private void Update()
    {
        if (elapsed > 0)
        {
            var x = Mathf.PerlinNoise(Time.time * 10, 0) * 2 - 1;
            var y = Mathf.PerlinNoise(0, Time.time * 10) * 2 - 1;

            transform.position = new Vector3(x * magnitude, y * magnitude, transform.position.z);
            elapsed -= Time.deltaTime;
            if (elapsed <= 0)
            {
                transform.position = Vector3.zero; 
            }
        }
    }
}