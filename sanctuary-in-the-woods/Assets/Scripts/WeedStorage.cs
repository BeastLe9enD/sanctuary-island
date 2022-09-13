using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using JetBrains.Annotations;
using UnityEngine;

public class WeedStorage : MonoBehaviour {
    [ItemCanBeNull] public StackedItem Slot { get; set; }
    public Rigidbody Rigidbody;
    public GameObject Player;
    public PlayerInventory PlayerInventory;
    public ItemRegistry ItemRegistry;

    private bool CanAttactAnimals() {
        if (Slot == null) return false;
        return Slot.Item.Equals(ItemRegistry.Weed);
    }
    
    void Start() {
        Rigidbody = GetComponent<Rigidbody>();

        Player = GameObject.Find("Player");
        PlayerInventory = FindObjectOfType<PlayerInventory>();
        ItemRegistry = FindObjectOfType<ItemRegistry>();
    }

    private void OnMouseOver() {
        if (!Input.GetMouseButtonDown(2)) return;

        var toRemove = new StackedItem(ItemRegistry.Weed);
        
        Debug.Log("Clicked");

        if (PlayerInventory.CanRemove(toRemove)) {
            Debug.Log("Can be removed");
            
            PlayerInventory.Remove(toRemove);
            PlayerInventory.
        }

        Slot = toRemove;
    }

    void AttractAnimals() {
        if (!CanAttactAnimals()) return;
    }
    
    // Update is called once per frame
    void Update()
    {
        AttractAnimals();
    }
}
