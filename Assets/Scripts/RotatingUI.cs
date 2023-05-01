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
                case "flash" : images[i].GetComponent<Image>().sprite = megaPackageScript.sprites["flash"];
                break;
                case "radioactive" : images[i].GetComponent<Image>().sprite = megaPackageScript.sprites["radioactive"];
                break;
                case "tv" : images[i].GetComponent<Image>().sprite = megaPackageScript.sprites["tv"];
                break;
                case "vase" : images[i].GetComponent<Image>().sprite = megaPackageScript.sprites["vase"];
                break;
                case "woodenBox" : images[i].GetComponent<Image>().sprite = megaPackageScript.sprites["woodenBox"];
                break;
                default : Debug.Log("Nincs ilyen nevű item!");
                break;
            }
            
        }   
    }
}
