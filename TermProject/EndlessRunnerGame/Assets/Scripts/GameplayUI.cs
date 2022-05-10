using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI lives;
    // Start is called before the first frame update
    void Start()
    {
        distance.text = "Distance: " + 0 + "ft";
        coins.text = "Coins: " + 0;
        lives.text = "Lives: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDistance(float distances)
    {
        distance.text = "Distance: " + (int)distances + "ft";
    }

    public void updateCoin(int coinCount)
    {
        coins.text = "Coins: " + coinCount;
    }

    public void updateHealth(int numlives)
    {
        lives.text = "Lives: " + numlives;
    }
}
