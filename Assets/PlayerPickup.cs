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
    
    void OnItemSelect(SelectableItem selectableItem){
        selectableItem.ItemSelected();
    }

    void OnItemUnselect(SelectableItem selectableItem){
        selectableItem.ItemUnselected();
    }

    void OnItemActivated(SelectableItem selectableItem){
        selectableItem.SetItemPosition(transform.position);
        activatedItem.ItemSelected();
    }


    void Update(){
        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);

        if (!activatedItem && Physics.Raycast(ray, out RaycastHit raycastHit, itemPickupLength, layerToCheck))
        {
            
            if(raycastHit.collider.gameObject.TryGetComponent<SelectableItem>(out var selectableItem))
            {
                OnItemSelect(selectableItem);
                if(Input.GetKeyDown(KeyCode.E)){
                    activatedItem = selectableItem;
                    activatedItem.ItemActivated(transform.position);
                }
            }
            if(lastHitItem && lastHitItem != selectableItem){
                OnItemUnselect(lastHitItem);
            }
            lastHitItem = selectableItem;
        } else { //if the raycast does not hit
            if(lastHitItem && lastHitItem.isSelected == true){
                OnItemUnselect(lastHitItem);
                lastHitItem = null;
            }
        }

        if(activatedItem != null){
            OnItemActivated(activatedItem);

            if(Input.GetKeyUp(KeyCode.E)){
                OnItemUnselect(activatedItem);
                activatedItem = null;
            }
        }

    }

}
