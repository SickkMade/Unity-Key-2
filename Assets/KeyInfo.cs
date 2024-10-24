using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInfo : MonoBehaviour
{
    private bool isCorrect = false;

    public void SetKeyCorrect(){
        isCorrect = true;
    }

    public bool CheckCorrect(){
        return isCorrect;
    }
}
