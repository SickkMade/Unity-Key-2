using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField, Range(0,50)]
    private float itemPickupLength = 5f;
    [SerializeField]
    LayerMask layerToCheck;
    [SerializeField]
    private Camera playerCamera;

    private SelectableItem lastHitItem;
    private SelectableItem activatedItem;

    void Update(){
        if(activatedItem && activatedItem.isHeld){
            HoldItem();
        }
        else{
            Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastForSelection(ray);
            RaycastForHolding(ray);
        }
    }

    private void RaycastForHolding(Ray ray){
        if (Physics.Raycast(ray, out RaycastHit raycastHit, itemPickupLength, layerToCheck)){
            if(raycastHit.collider.gameObject.TryGetComponent<SelectableItem>(out var selectableItem))
            {
                if(Input.GetKeyDown(KeyCode.E)){
                    activatedItem = selectableItem;
                    activatedItem.ItemActivated(this.transform.position);
                    activatedItem.isHeld = true;
                }
            }
        }
        
    }

    private void HoldItem(){
        if(Input.GetKeyUp(KeyCode.E)){
            DropItem();
        }
        else{
            activatedItem.SetItemPosition(this.transform.position);
        }
    }

    private void RaycastForSelection(Ray ray){
        if (Physics.Raycast(ray, out RaycastHit raycastHit, itemPickupLength, layerToCheck))
        {
            if(raycastHit.collider.gameObject.TryGetComponent<SelectableItem>(out var selectableItem))
            {
                selectableItem.ItemSelected();
            }
            if(lastHitItem && lastHitItem != selectableItem){
                lastHitItem.ItemUnselected();
            }
            lastHitItem = selectableItem;
        } else { //if the raycast does not hit
            if(lastHitItem && lastHitItem.isSelected == true){
                lastHitItem.ItemUnselected();
                lastHitItem = null;
            }
        }
    }

    public void DropItem(){
        if(!activatedItem) return;
        activatedItem.isHeld = false;
        activatedItem.ItemUnselected();
        activatedItem = null;
    }
}
