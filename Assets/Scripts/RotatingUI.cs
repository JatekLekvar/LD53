using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatingUI : MonoBehaviour
{
    [SerializeField] private MegaPackage megaPackageScript;
    [SerializeField] private GameObject[] images;

    void Awake(){
        megaPackageScript = this.gameObject.GetComponentInParent<MegaPackage>();
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

        for (int i = 0; i < megaPackageScript.items.Length; i++){
            switch(megaPackageScript.items[i]){
                case "barrel" : images[i].GetComponent<Image>().sprite = megaPackageScript.sprites["barrel"];
                break;
                default : Debug.Log("Nincs ilyen nevű item!");
                break;
            }
            
        }   
    }
}
