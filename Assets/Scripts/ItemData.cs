using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/New Item")]
public class ItemData : ScriptableObject
{
    public string name; // Nom de l'objet
    public Sprite visual; // Sprite associé à l'objet
    public GameObject prefab; // Prefab à instancier
}
