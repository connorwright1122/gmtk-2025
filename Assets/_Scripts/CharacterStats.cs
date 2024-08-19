using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float size;
    //public float 

    public float GetSize()
    {
        return this.transform.localScale.x;
    }

    public void SetSize()
    {
        size = this.transform.localScale.x;
    }
}
