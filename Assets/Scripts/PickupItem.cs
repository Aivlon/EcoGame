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
    private GameObject toucheE; // L'indicateur "E"

    private RaycastHit currentHit; // Pour stocker l'objet détecté
    private bool isFacingItem = false; // Savoir si on fait face à un objet

    void Update()
    {
        // Vérifie si un objet est devant nous
        CheckForItem();

        // Si on est face à un objet et qu'on appuie sur "E"
        if (isFacingItem && Input.GetKeyDown(KeyCode.E))
        {
            PickupCurrentItem();
        }
    }

    private void CheckForItem()
    {
        // Lance un raycast pour détecter un objet
        if (Physics.Raycast(transform.position, transform.forward, out currentHit, pickupRange, layerMask))
        {
            // Si l'objet détecté a le tag "Item"
            if (currentHit.transform.CompareTag("Item"))
            {
                Debug.Log("Item détecté devant nous.");
                toucheE.SetActive(true); // Active l'indicateur
                isFacingItem = true;
                return;
            }
        }

        // Si aucun objet n'est détecté ou on n'est plus face à un objet
        toucheE.SetActive(false);
        isFacingItem = false;
    }

    private void PickupCurrentItem()
    {
        // Vérifie que l'objet détecté est valide
        if (currentHit.transform != null)
        {
            Item itemComponent = currentHit.transform.GetComponent<Item>();

            if (itemComponent != null)
            {
                Debug.Log($"Ramassé : {itemComponent.item.name}");

                // Ajoute l'objet à l'inventaire
                Inventory.AddItem(itemComponent.item);

                // Détruit l'objet dans la scène
                Destroy(currentHit.transform.gameObject);

                // Désactive l'indicateur
                toucheE.SetActive(false);
                isFacingItem = false;
            }
            else
            {
                Debug.LogWarning("Aucun composant Item trouvé sur l'objet.");
            }
        }
        else
        {
            Debug.LogWarning("Aucun objet valide à ramasser.");
        }
    }
}
