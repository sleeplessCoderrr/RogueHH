using System.Threading.Tasks;
using UnityEngine;

public class PlayerDirector : MonoBehaviour
{
    public static PlayerDirector Instance;

    [Header("Player Config & Data")]
    public PlayerConfig playerConfig;
    public PlayerData playerData;
    
    [Header("Importing Map Data")]
    public MapConfig mapConfig;
    public MapData mapData;
    
    private PlayerBuilder _playerBuilder;
    public Player player;
    
    public Animator animator;
    public GameObject playerInstance;
    
    private void Start()
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
        
        InitializePlayer();
    }

    private async void InitializePlayer()
    {
        await Task.Delay(1000);
        _playerBuilder = new PlayerBuilder();
        player = new Player(playerConfig, playerData, animator);

        _playerBuilder.SetParent(transform);
        playerInstance = _playerBuilder
            .SetData(player.PlayerConfig, player.PlayerData)
            .Build(mapConfig, mapData, 1)[0];
    }
}