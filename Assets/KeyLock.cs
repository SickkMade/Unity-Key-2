using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.gameObject.TryGetComponent<KeyInfo>(out var keyInfo)){
            if(keyInfo.CheckCorrect()){ //if its the key
                Debug.Log("win");
            }
            else{
                Debug.Log("false");
                //temp test
                other.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,5) * 3, Random.Range(0,5) * 3, Random.Range(0,5) * 3), ForceMode.Impulse);
            }
        }
    }
}
