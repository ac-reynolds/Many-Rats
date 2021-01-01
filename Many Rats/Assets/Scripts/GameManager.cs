using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int cheeseAvailable;
    public static int score;
    public Text cheeseText;
    public Text scoreText;

    private bool _witchHasSpawned = false;
    private bool _personHasSpawned = false;
    private bool _carriageLoaded = false;

    private void Start() {
        EventManagerOneArg<SpawnWitchEvent, GameObject>.GetInstance().AddListener(OnWitchSpawn);
        EventManagerOneArg<SpawnPersonEvent, GameObject>.GetInstance().AddListener(OnPersonSpawn);
        EventManagerZeroArgs<CarriageLoadingSuccessfulEvent>.GetInstance().AddListener(OnCarriageLoad);

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
    }

    // increases score by 1, called during PersonBehavior event PersonDelivered
    public void IncreaseScore() {
        score++;
    }

    // increases cheese by 1, called during PersonBehavior event PersonDelivered
    public void IncreaseCheese() {
        cheeseAvailable++;
    }
}