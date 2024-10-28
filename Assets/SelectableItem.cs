using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItem : MonoBehaviour
{
    private Renderer itemRenderer;

    [HideInInspector]
    public bool isSelected = false;
    public bool isHeld = false;

    private Rigidbody rb;

    private float distanceFromPlayer = 1;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    public void ItemSelected(){
        isSelected = true;
        SetMaterialRecursivley(Color.yellow, this.transform);
    }

    public void ItemActivated(Vector3 playerPosition){
        isHeld = true;
        distanceFromPlayer = Vector3.Distance(playerPosition, transform.position);
    }

    public void SetItemPosition(Vector3 playerPosition){
        rb.velocity = Vector3.zero;
        transform.position = Vector3.Lerp(transform.position, playerPosition + Camera.main.transform.forward * distanceFromPlayer, .3f);
    }

    public void ItemUnselected(){
        isSelected = false;
        isHeld = false;
        SetMaterialRecursivley(Color.white, this.transform);
    }

    private void SetMaterialRecursivley(Color color, Transform obj){
        itemRenderer = obj.GetComponent<Renderer>();

        if(itemRenderer != null){
            itemRenderer.material.color = color;
        }
        foreach(Transform child in obj){
            SetMaterialRecursivley(color, child);
        }
    }
}
