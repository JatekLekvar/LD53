using System.Collections.Generic;
using UnityEngine;

public class PackagingTrigger : MonoBehaviour
{
    public GameObject megaPackagePrefab;
    public GameObject palletPrefab;

    List<Item> items = new List<Item>();
    private List<GameObject> pallets = new List<GameObject>();
    Transform spawnpoint;
    Transform palletSpawnPoint;

    void Start()
    {
        spawnpoint =  transform.Find("SpawnPoint");
        palletSpawnPoint =  transform.Find("PalletSpawnPoint");
    }

    void OnTriggerEnter(Collider collider)
    {
        Item item = collider.gameObject.GetComponent<Item>();
        if (item != null)
        {
            if (item.identifier == "package")
            {
                OnPackageAdded(item);
            }
            else
            {
                items.Add(item);
                Debug.Log($"Added {item.identifier}");
            }

        }
        if(collider.gameObject.tag == "Pallet"){
            Debug.Log("Pallet added");
            pallets.Add(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Item item = collider.gameObject.GetComponent<Item>();
        if (item != null)
        {
            items.Remove(item);
            Debug.Log($"Removed {item.identifier}");
        }
    }

    void OnPackageAdded(Item package)
    {
        Quaternion rot = transform.rotation;

        foreach (GameObject obj in pallets)
        {
            GameObject.Destroy(obj);
        }

        GameObject mp = Instantiate(megaPackagePrefab, spawnpoint.position, rot);
        MegaPackage mpc = mp.GetComponent<MegaPackage>();
        mpc.items = items.ConvertAll(i => i.identifier).ToArray();

        rot = Quaternion.Euler(Vector3.up * 0f);

        Instantiate(palletPrefab, spawnpoint.position - Vector3.up * 0.5f, rot);
        Destroy(package.gameObject);
        foreach (Item item in items)
        {
            Destroy(item.gameObject);
        }

        pallets = new List<GameObject>();
        items = new List<Item>();
    }
}
