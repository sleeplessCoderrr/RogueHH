using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    public Transform player; 
    public LayerMask obstacleMask; 
    public float detectionRange = 10f; 
    public bool playerInSight;
    public bool activate;

    private void Update()
    {
        if(activate) CheckLineOfSight();
    }

    private void CheckLineOfSight()
    {
        playerInSight = false; 
        var directionToPlayer = (player.position - transform.position).normalized;
        var distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange, obstacleMask))
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

        Debug.DrawLine(transform.position, player.position, playerInSight ? Color.green : Color.red);
    }

    public bool GetResult()
    {
        return playerInSight;
    }
}