using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objectToSpawn;
    
    [SerializeField, Range(0, 1000)]
    private int amountToSpawn = 5;
    [SerializeField, Range(0, 5)]
    private float spawnInterval = 0.5f;
    private int correctKey;

    void Start(){
        StartCoroutine(SpawnCoroutine());
        correctKey = Random.Range(0, amountToSpawn-1);
    }

    IEnumerator SpawnCoroutine(){
        for(int i = 0; i < amountToSpawn; i++){
            yield return new WaitForSeconds(spawnInterval);
            int randomValue = Random.Range(0, objectToSpawn.Count);
            var key = Instantiate(objectToSpawn[randomValue], transform);

            if(i == correctKey){
                key.GetComponent<KeyInfo>().SetKeyCorrect();
            }
        }
    }
}
