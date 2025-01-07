using UnityEngine;
using UnityEngine.Serialization;

public class LookAtEnemy : MonoBehaviour
{
    public Transform player; 
    public bool isActive = false;
    public float rotationSpeed = 100f;

    private void Update()
    {
        if (player != null && isActive)
        {
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        var directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0;
        var targetRotation = Quaternion.LookRotation(directionToPlayer);

        if (rotationSpeed > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = targetRotation;
        }
        
        isActive = false;
    }
}