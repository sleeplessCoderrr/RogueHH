using System.Collections;
using UnityEngine;

public class Player
{
    [Header("Player SO")]
    public PlayerConfig PlayerConfig;
    public PlayerData PlayerData;
    
    [Header("Additional Data")]
    public GameObject PlayerInstance;
    public Vector3 TargetPosition;
    public Animator Animator;

    public Player(PlayerConfig playerConfig, PlayerData playerData)
    {
        this.PlayerConfig = playerConfig;
        this.PlayerData = playerData;
    }

    public IEnumerator MoveToTarget(Player player, GameObject playerInstance, Vector3 targetPosition)
    {
        while (Vector3.Distance(playerInstance.transform.position, targetPosition) > 0.01f)
        {
            player.WalkToward(targetPosition);
            UpdateData(playerInstance);
            
            yield return null;
        }
        
    }

    private void WalkToward(Vector3 targetPosition)
    {
        var rigidbody = PlayerInstance.GetComponent<Rigidbody>();
        var direction = (targetPosition - PlayerInstance.transform.position).normalized;
        rigidbody.velocity = direction * PlayerConfig.walkSpeed;

        if (Vector3.Distance(PlayerInstance.transform.position, targetPosition) <= 0.1f)
        {
            rigidbody.velocity = Vector3.zero;
            PlayerInstance.transform.position = targetPosition;
        }
    }
    
    public void LookAtTarget(GameObject playerInstance, Vector3 targetPosition)
    {
        var directionToTarget = (targetPosition - playerInstance.transform.position).normalized;
        if (directionToTarget.sqrMagnitude > 0.01f)
        {
            var targetRotation = Quaternion.LookRotation(directionToTarget);
            playerInstance.transform.rotation = Quaternion.Slerp(
                playerInstance.transform.rotation, 
                targetRotation, 
                Time.deltaTime * 100000f
            );
        }
    }
    
    public void UpdateData(GameObject playerInstance)
    {
        PlayerData.playerPosition = new Vector3(
            playerInstance.transform.position.x,
            playerInstance.transform.position.y,
            playerInstance.transform.position.z
        );
    }

}
