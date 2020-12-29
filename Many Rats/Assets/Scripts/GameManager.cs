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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cheeseText.text = cheeseAvailable + " Cheese";
        scoreText.text = "Score:" + score;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public void IncreaseCheese()
    {
        cheeseAvailable++;
    }
}
