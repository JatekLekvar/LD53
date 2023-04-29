using System.Collections.Generic;
using UnityEngine;

public class PackagingTrigger : MonoBehaviour
{
    public GameObject megaPackagePrefab;
    public GameObject palletPrefab;

    List<Item> items = new List<Item>();
    Transform spawnpoint;

    void Start()
    {
        spawnpoint = transform.GetChild(0);
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

        GameObject mp = Instantiate(megaPackagePrefab, spawnpoint.position, rot);
        MegaPackage mpc = mp.GetComponent<MegaPackage>();
        mpc.items = items.ConvertAll(i => i.identifier).ToArray();

        Instantiate(palletPrefab, spawnpoint.position - Vector3.up * 0.5f, rot);

        Destroy(package.gameObject);
        foreach (Item item in items)
        {
            Destroy(item.gameObject);
        }
    }
}
