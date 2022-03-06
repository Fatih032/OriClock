using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ekle : MonoBehaviour
{
    public GameObject Sporcu;
    bool buttonActive = false;
    Vector3 eklevektör = new Vector3(0, -1, 0);
    
    void Update()
    {
        if(buttonActive){
            GameObject yeni_sporcu = Instantiate(Sporcu, Sporcu.transform.position ,Quaternion.identity);
            buttonActive=false;

        }

     
    }


    /*public void ekleSporcu()
    {
        buttonActive = true;
        yenideğer += new Vector3(Sporcu.transform.position + eklevektör);
    }*/
}
