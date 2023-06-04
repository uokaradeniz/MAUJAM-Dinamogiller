using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanSpawner : MonoBehaviour
{
    public float spawnDurationMax;
    public float spawnDurationMin;
    private float spawnDuration;
    private float spawnTimer;

    private GameHandler gameHandler;

    private void Start()
    {
        gameHandler = GameObject.Find("Game Handler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHandler.wonGame)
        {
            spawnDuration = Random.Range(spawnDurationMin, spawnDurationMax);
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDuration)
                SpawnHuman();
        }
    }

    void SpawnHuman()
    {
        spawnTimer = 0;
        Instantiate(Resources.Load("Human"), transform.position, Quaternion.identity);
    }
}
