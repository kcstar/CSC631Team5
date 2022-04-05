using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform level_1;
    [SerializeField] private Transform level_2;
    [SerializeField] private Transform level_3;
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
        Transform levelPartTransform;
        Transform selectedLevel;
        int rand = Random.Range(1, 4);

        switch (rand)
        {
          case 1:
            Debug.Log($"Spawning level {rand} prefab...");
            selectedLevel = level_1;
            break;
          
          case 2:
            Debug.Log($"Spawning level {rand} prefab...");
            selectedLevel = level_2;
            break;
          
          case 3:
            Debug.Log($"Spawning level {rand} prefab...");
            selectedLevel = level_3;
            break;
          
          default:
            Debug.Log("Spawning level 1 prefab...");
            selectedLevel = level_1;
            break;
        }
        
        levelPartTransform = Instantiate(selectedLevel, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
