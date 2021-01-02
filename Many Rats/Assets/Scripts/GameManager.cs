using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static int cheeseAvailable;
    public static int score;
    public static int lives = 3;
    public Text cheeseText; 
    public Text scoreText;
    public Text livesText;

    private bool _witchHasSpawned = false;
    private bool _personHasSpawned = false;
    private bool _carriageLoaded = false;

    public UnityEvent gameOver;

    public Text gameOverText;

    private void Start() {
        EventManagerOneArg<SpawnWitchEvent, GameObject>.GetInstance().AddListener(OnWitchSpawn);
        EventManagerOneArg<SpawnPersonEvent, GameObject>.GetInstance().AddListener(OnPersonSpawn);
        EventManagerZeroArgs<CarriageLoadingSuccessfulEvent>.GetInstance().AddListener(OnCarriageLoad);
        lives = 3;
        score = 0;
        cheeseAvailable = 0;
    }

    private void OnCarriageLoad() {
        if (!_carriageLoaded) {
            _carriageLoaded = true;
            EventManagerZeroArgs<TriggerDialogue3Event>.GetInstance().InvokeEvent();
        }
    }

    private void OnPersonSpawn(GameObject person) {
        if (!_personHasSpawned) {
            _personHasSpawned = true;
            EventManagerZeroArgs<TriggerDialogue2Event>.GetInstance().InvokeEvent();
        }
    }

    private void OnWitchSpawn(GameObject witch) {
        if (!_witchHasSpawned) {
            _witchHasSpawned = true;
            EventManagerZeroArgs<TriggerDialogue4Event>.GetInstance().InvokeEvent();
        }
    }

    // updates HUD text with score and cheese available for right click
    void Update() {
        cheeseText.text = cheeseAvailable + " Cheese";
        scoreText.text = "Score:" + score;
        livesText.text = "Lives:" + lives;

        if(lives<=0)
        {
            gameOver.Invoke();
            gameOverText.text = "Why did only " + score + " people make it to the town hall meeting, Ratgusher?! Where is everyone else?!";
        }
    }

    // increases score by 1, called during PersonBehavior event PersonDelivered
    public void IncreaseScore() {
        score++;
    }

    // increases cheese by 1, called during PersonBehavior event PersonDelivered
    public void IncreaseCheese() {
        cheeseAvailable++;
    }

    public void LoseLives()
    {
        lives--;
    }

    public void ResetGame()
    {

    }

}