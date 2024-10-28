using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    [SerializeField]
    private GameObject keyEmpty;
    [SerializeField]
    private Animator doorAnimator; 
    [SerializeField, Range(0, 5)]
    private int keyCooldown = 1;
    private GameObject currentKey = null;
    private Rigidbody keyRigidbody = null;
    void OnTriggerEnter(Collider other){
        //this is so unreadable
        if(currentKey == null && other.gameObject.TryGetComponent<KeyInfo>(out var keyInfo)){
            currentKey = other.gameObject;
            currentKey.GetComponent<SelectableItem>().isHeld = false;
            keyRigidbody = currentKey.GetComponent<Rigidbody>();
            //animation
            currentKey.transform.SetParent(keyEmpty.transform);
            currentKey.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            keyRigidbody.useGravity = false;
            keyRigidbody.isKinematic = true;
            doorAnimator.SetTrigger("KeyTurn");


            if(keyInfo.CheckCorrect()){ //if its the key
                Debug.Log("win");
            }
            else{
                Debug.Log("false");
            }
        }
    }


    public void DropKey(){
        if(currentKey == null) return;
        keyRigidbody.useGravity = true;
        keyRigidbody.isKinematic = false;
        currentKey.transform.SetParent(null);
        StartCoroutine(DropKeyOffset());
    }

    private IEnumerator DropKeyOffset(){
        yield return new WaitForSeconds(keyCooldown);
        currentKey = null;
        keyRigidbody = null;
    }

}
