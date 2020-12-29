using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int cheeseAvailable;
    public static int score;
    [SerializeField] private Text cheeseText;
    [SerializeField] private Text scoreText;

    // updates HUD text with score and cheese available for right click
    void Update()
    {
        cheeseText.text = cheeseAvailable + " Cheese";
        scoreText.text = "Score:" + score;
    }

    // increases score by 1, called during PersonBehavior event PersonDelivered
    public void IncreaseScore()
    {
        score++;
    }

    // increases cheese by 1, called during PersonBehavior event PersonDelivered
    public void IncreaseCheese()
    {
        cheeseAvailable++;
    }
}
