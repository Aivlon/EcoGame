using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<ItemData> content = new List<ItemData>(); // Liste des objets de l'inventaire

    [SerializeField]
    private GameObject inventoryPanel; // Panneau de l'inventaire dans l'UI

    [SerializeField]
    private Transform inventorySlotsParent; // Parent des slots de l'inventaire

    // Ajoute un objet dans l'inventaire
    public void AddItem(ItemData item)
    {
        content.Add(item); // Ajoute l'objet à la liste
        Debug.Log($"Ajouté : {item.name}");
        RefreshContent();
    }

    const int InventorySize = 15;

    private void Start()
    {
        RefreshContent();
    }

    void Update()
    {
        // Ouvre ou ferme l'inventaire avec la touche "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    private void RefreshContent()
    {
        // Parcourt tous les items dans l'inventaire
        for (int i = 0; i < content.Count; i++)
        {
            if (i < inventorySlotsParent.childCount) // Vérifie qu'il y a assez de slots
            {
                // Accède au slot et à son image
                Transform slot = inventorySlotsParent.GetChild(i);
                Image image = slot.GetChild(0).GetComponent<Image>();

                if (image != null)
                {
                    image.sprite = content[i].visual; // Assigne le sprite
                    image.enabled = true; // Assure que l'image est visible
                    Debug.Log($"Sprite '{content[i].visual?.name}' assigné au slot {i}.");
                }
                else
                {
                    Debug.LogWarning($"Pas de composant Image trouvé pour le slot {i}.");
                }
            }
            else
            {
                Debug.LogWarning($"Pas assez de slots pour afficher l'item {content[i].name}.");
            }
        }
    }

    public bool IsFull()
    {
        return InventorySize == content.Count;
    }
}
    
