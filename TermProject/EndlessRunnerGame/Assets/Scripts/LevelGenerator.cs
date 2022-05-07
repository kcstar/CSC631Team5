using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform background_start;
    [SerializeField] private Transform level_start;
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private Transform[] levels;
    private Vector3 lastEndPosition;
    private const float PLAYER_DISTANCE_SPAWN_LEVEL = 100f;
    private int prevLevel = 3;

    private void Awake()
    {
        lastEndPosition = level_start.Find("EndPosition").position;
    }

    private void Update()
    {
        if (Vector3.Distance(GameObject.Find("Player").transform.position , lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL)
        {
            generateLevel();
        }
    }

    private void generateLevel()
    {
        Transform lastBackgroundTransform = spawnBackground(lastEndPosition);
        Transform lastLevelTransform = spawnLevel(lastEndPosition);
        lastEndPosition = lastLevelTransform.Find("EndPosition").position;
    }

    private Transform spawnBackground(Vector3 spawnPosition)
    {
        int selectedBackground = Random.Range(1, backgrounds.Length);
        Transform backgroundPartTransform = Instantiate(backgrounds[selectedBackground], spawnPosition + (new Vector3(-1.144409e-05f, 0, 121.85779f)), Quaternion.identity);
        Debug.Log($"Spawning {backgrounds[selectedBackground].name}...");
        return backgroundPartTransform;
    }
    private Transform spawnLevel(Vector3 spawnPosition)
    {
        int selectedLevel = Random.Range(1, levels.Length);
        Transform levelPartTransform = Instantiate(levels[selectedLevel], spawnPosition + (new Vector3(2.4728f, 0, 65.85778f)), Quaternion.identity);
        prevLevel = selectedLevel;
        Debug.Log($"Spawning {levels[selectedLevel].name}...");
        return levelPartTransform;
    }
}
