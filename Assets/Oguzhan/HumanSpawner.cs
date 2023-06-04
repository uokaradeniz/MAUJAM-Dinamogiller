using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public float spawnDurationMax;
    public float spawnDurationMin;
    private float spawnDuration;
    private float spawnTimer;

    // Update is called once per frame
    void Update()
    {
        spawnDuration = Random.Range(spawnDurationMin, spawnDurationMax);
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDuration)
            SpawnHuman();
    }

    void SpawnHuman()
    {
        spawnTimer = 0;
        Instantiate(Resources.Load("Human"), transform.position, Quaternion.identity);
    }
}
