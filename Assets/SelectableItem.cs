using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItem : MonoBehaviour
{
    private Renderer itemRenderer;

    [HideInInspector]
    public bool isSelected = false;

    private Rigidbody rb;

    private float distanceFromPlayer = 1;

    void Start(){
        itemRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    public void ItemSelected(){
        isSelected = true;
        if(itemRenderer != null){
            itemRenderer.material.color = Color.blue;
        }
    }

    public void ItemActivated(Vector3 playerPosition){
        distanceFromPlayer = Vector3.Distance(playerPosition, transform.position);
    }

    public void SetItemPosition(Vector3 playerPosition){
        transform.position = Vector3.Lerp(transform.position, playerPosition + Camera.main.transform.forward * distanceFromPlayer, .3f);
        rb.velocity = Vector3.zero;
    }

    public void ItemUnselected(){
        isSelected = false;
        if(itemRenderer != null){
            itemRenderer.material.color = Color.red;
        }
    }
}
