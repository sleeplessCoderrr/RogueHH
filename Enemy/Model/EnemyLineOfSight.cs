using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    public float eyeLevelOffset = 1.5f;
    public float detectionRange = 10f; 
    public LayerMask obstacleMask; 
    public bool playerInSight;
    public Transform player; 
    public bool activate;

    private void Update()
    {
        if (activate) CheckLineOfSight();
        // Debug.Log("Live report: " + playerInSight);
    }

    private void CheckLineOfSight()
    {
        playerInSight = false;
        var rayOrigin = transform.position + Vector3.up * eyeLevelOffset; 
        var playerEyeLevel = player.position + Vector3.up * eyeLevelOffset; 
        var directionToPlayer = (playerEyeLevel - rayOrigin).normalized;
        var distanceToPlayer = Vector3.Distance(rayOrigin, playerEyeLevel);

        if (distanceToPlayer <= detectionRange)
        {
            if (!Physics.Raycast(rayOrigin, directionToPlayer, out RaycastHit hit, detectionRange, obstacleMask))
            {
                playerInSight = true;
            }
            else
            {
                if (hit.transform == player)
                {
                    playerInSight = true;
                }   
            }
        }

        Debug.DrawLine(rayOrigin, playerEyeLevel, playerInSight ? Color.green : Color.red);
    }

    public bool GetResult()
    {
        return playerInSight;
    }
}