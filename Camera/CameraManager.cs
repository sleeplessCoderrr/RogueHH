using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("Camera")]
    private PlayerData _playerData;
    private Vector3 _velocity = Vector3.zero;

    private Vector3 _originalPosition;
    private bool _isShaking = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        await Task.Delay(1000);
        _playerData = PlayerDirector.Instance.playerData;
        _originalPosition = transform.position; // Store the initial position
    }

    private void Update()
    {
        if (_isShaking) return; // Skip position updates during shake

        _playerData = PlayerDirector.Instance.playerData;
        var targetPosition = new Vector3(
            (_playerData.playerPosition.x) - 10,
            10,
            (_playerData.playerPosition.z) - 10
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            0.5f
        );
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        _isShaking = true;
        var elapsed = 0.0f;

        while (elapsed < duration)
        {
            var x = Random.Range(-1f, 1f) * magnitude;
            var y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = _originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _originalPosition; 
        _isShaking = false;
    }

    public void TriggerShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
}