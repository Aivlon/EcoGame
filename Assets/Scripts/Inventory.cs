using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] // Permet de configurer dans l'Inspector
    public List<ItemData> content = new List<ItemData>(); // Liste des objets de l'inventaire

    [SerializeField] // Référence au panneau d'inventaire dans l'UI
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotsParent;

    // Ajoute un objet dans l'inventaire
    public void AddItem(ItemData item)
    {
        content.Add(item); // Ajoute l'objet à la liste
        Debug.Log($"Ajouté : {item.name}");
        RefreshContent();
    }

    void Update()
    {
        // Ouvrir ou fermer l'inventaire avec la touche "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    private void RefreshContent()
    {
        for (int i = 0; i < content.Count; i++)
        {
            inventorySlotsParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = content[i].visual;
        }
    }
}
