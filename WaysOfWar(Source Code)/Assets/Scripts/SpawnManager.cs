using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    // Var
    public GameObject enemyPrefab;
    public GameObject HealthPotionPrefab;
    public float spawnRange = 100;
    public int waveNumber = 1;
    public int enemyCount;
    public TextMeshProUGUI Mission1;
    public TextMeshProUGUI Mission2;
    public GameObject Gate;
    public GameObject invisibleGate;
    public GameObject Gate2;
    public GameObject invisibleGate2;


    void Start()
    {
        
        
        Instantiate(HealthPotionPrefab, HGenerateSpawnPosition(), HealthPotionPrefab.transform.rotation);
        SpawnEnemyWave(waveNumber);
        


    }

    
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        //If enemy is O do this 
        if (enemyCount == 0)
        {
            Instantiate(HealthPotionPrefab, HGenerateSpawnPosition(), HealthPotionPrefab.transform.rotation);
            Debug.Log("U defeated the wave");
            Mission1.gameObject.SetActive(false);
            Mission2.gameObject.SetActive(true);
            Gate.gameObject.SetActive(false);
            invisibleGate.gameObject.SetActive(false);
            Gate2.gameObject.SetActive(false);
            invisibleGate2.gameObject.SetActive(false);


            GetComponent<SpawnManager>().enabled = false;

        }
    }
    
    //Spawn Postions  for enemy
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;

    }








    //Spawn for health Potiton
    private Vector3 HGenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);

        return randomPos;

    }










    //Genarate Enemy
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i =0; i< enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }








}
