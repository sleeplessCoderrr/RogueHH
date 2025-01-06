using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "SO/Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("Enemy Components")] 
    public GameObject[] enemyPrefabs;
    public Animator animator;
    
    [Header("Enemy Settings")]
    public float walkSpeed = 5f;
    public string[] initials = new string[] 
    {
        "AC", "AS", "BD", "BT", "CT", "FO", "GN", "GY", "HO", "KH", "MM", "MR", "MV", "NS", "OV",
        "PL", "RU", "TI", "VD", "VM", "WS", "WW", "YD"
    };

}