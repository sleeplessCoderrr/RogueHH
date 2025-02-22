﻿using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Vector3Int enemyPosition;
    public float attack = 5f;
    public float defense = 3f;
    public float maxHealth = 20f;
    public float currentHealth = 20f;

    public GameObject instance;
    public Enemy Enemy;

    public bool isTurn = false;
}
