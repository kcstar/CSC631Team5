using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] levels;
    [SerializeField] private Transform level_start;
    private Vector3 lastEndPosition;
    private const float PLAYER_DISTANCE_SPAWN_LEVEL = 30f;


    private void Awake()
    {
        lastEndPosition = level_start.Find("EndPosition").position;
        //spawnLevelPart();
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
        int selectedLevel = Random.Range(0, levels.Length);
        //Transform levelPartTransform = Instantiate(levels[selectedLevel], spawnPosition, Quaternion.identity);
        Transform levelPartTransform = Instantiate(levels[selectedLevel], spawnPosition + (new Vector3(36, 0, 0)), Quaternion.identity);

        Debug.Log($"Spawning level {selectedLevel} prefab...");
        return levelPartTransform;
    }
}
