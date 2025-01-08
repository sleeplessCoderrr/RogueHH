using System.Collections;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;
    public bool isActive = false;
    public float rotationSpeed = 10f;
    private CoroutineManager _lookAtCoroutine;

    private void Update()
    {
        if (player != null && isActive)
        {
            if (_lookAtCoroutine == null)
            {
                _lookAtCoroutine = CoroutineManager.Instance;
                _lookAtCoroutine.StartManagedCoroutine(RotateLookAtPlayer());
            }
            isActive = false; 
        }
    }

    private IEnumerator RotateLookAtPlayer()
    {
        var directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0;
        var targetRotation = Quaternion.LookRotation(directionToPlayer);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }


        transform.rotation = targetRotation;
        _lookAtCoroutine = null;
    }
}