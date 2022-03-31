using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform level_1;
    [SerializeField] private Transform level_start;
    private Vector3 lastEndPosition;
    private const float PLAYER_DISTANCE_SPAWN_LEVEL = 15f;


    private void Awake()
    {
        lastEndPosition = level_start.Find("EndPosition").position;
        spawnLevelPart();
    }

    private void Update()
    {
        if (Vector3.Distance(GameObject.Find("Player").transform.position , lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL)
        {
            spawnLevelPart();
        }
    }

    private void spawnLevelPart()
    {
        Transform lastLevelTransform = spawnLevel(lastEndPosition);
        lastEndPosition = lastLevelTransform.Find("EndPosition").position;
    }
    private Transform spawnLevel(Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(level_1, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
