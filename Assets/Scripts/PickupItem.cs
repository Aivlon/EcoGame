using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private float pickupRange = 2.6f; // Distance pour ramasser un objet

    public Inventory Inventory; // Référence à l'inventaire

    [SerializeField]
    private LayerMask layerMask;
 
    [SerializeField]
    private GameObject toucheE;

    void Update()
    {
        RaycastHit hit;

        // Lance un raycast devant le joueur
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, layerMask))
        {
            // Vérifie si l'objet touché a le tag "Item"
            if (hit.transform.CompareTag("Item"))
            {
                Debug.Log("Item détecté devant nous.");

                // Vérifie si la touche "E" est pressée
                if (Input.GetKeyDown(KeyCode.E))
                {

                    toucheE.SetActive(true);
                    Item itemComponent = hit.transform.GetComponent<Item>();

                    if (itemComponent != null)
                    {
                        // Ajoute l'objet à l'inventaire
                        Debug.Log($"Ramassé : {itemComponent.item.name}");
                        Inventory.AddItem(itemComponent.item);

                        // Détruit l'objet dans la scène
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        Debug.LogWarning("Aucun composant Item trouvé sur l'objet.");
                    }
                }
            }
        }
        else 
        {
            toucheE.SetActive(false);
        }
    }
}
