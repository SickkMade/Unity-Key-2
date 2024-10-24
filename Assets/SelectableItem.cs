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

    public void SetItemPosition(Vector3 playerPosition){
        var posDiff = Vector3.Distance(playerPosition, transform.position);
        transform.position = playerPosition + Camera.main.transform.forward * posDiff;
        rb.velocity = Vector3.zero;
    }

    public void ItemUnselected(){
        isSelected = false;
        if(itemRenderer != null){
            itemRenderer.material.color = Color.red;
        }
    }
}
