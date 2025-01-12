using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupItem : MonoBehaviour
{
    [SerializeField] // Permet de modifier cette variable dans l'Inspector
    private float pickupRange = 2.6f; // Distance max pr ramasser un objet

    public Inventory Inventory; // Référence à l'inventaire du joueur

    void Update()
    {
        RaycastHit hit; 

         // Lance un raycast devant le joueur sur une certaine distance
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            // Vérifie si l'objet touché a le tag "Item"
            if (hit.transform.CompareTag("Item"))
            {
                Debug.Log("There is an item in front of us");

                //touche 
                if (Input.GetKeyDown(KeyCode.E))
                {
                      // Ajoute l'objet à l'inventaire
                    Item itemComponent = hit.transform.gameObject.GetComponent<Item>();
                    if (itemComponent != null)
                    {
                        // destroy the object
                        Inventory.content.Add(itemComponent.item);
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        Debug.LogWarning("No Item component found on the object.");
                    }
                }
            }
        }
    }
}
