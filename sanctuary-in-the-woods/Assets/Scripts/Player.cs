using System;
using Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    private Rigidbody2D rigidbody;
    private Vector2 playerDirection;
    private Tilemap topTilemap;

    void Start() {
        rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0.0f;
        rigidbody.freezeRotation = true;

        topTilemap = GameObject.Find("Top").GetComponent<Tilemap>();
    }

    void Movement() {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void DestroyTopTiles() {
        if (!Input.GetMouseButtonDown(0)) return;

        var (tile_pos, world_pos) = GetTileOnMouse();
        var distance = MathF.Abs(Vector2.Distance(world_pos, transform.position));

        if (distance > 2.0f) return;
        
        var oldTile = topTilemap.GetTile(tile_pos);
        if (oldTile == null) return;

        var itemRegistry = FindObjectOfType<ItemRegistry>();
        var playerInventory = FindObjectOfType<PlayerInventory>();
        
        playerInventory.Add(new StackedItem(itemRegistry.Weed, 1));
        
        topTilemap.SetTile(tile_pos, null);
    }
    
    // Update is called once per frame
    void Update() {
        Movement();
        DestroyTopTiles();

        if (Input.GetKeyDown(KeyCode.E))
        {
            var transform = GameObject.Find("Canvas").transform;
            var obj = transform.GetChild(1).gameObject;
            obj.SetActive(!obj.activeSelf);
        }
    }

    private void FixedUpdate() {
        rigidbody.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
    }

    private (Vector3Int, Vector2) GetTileOnMouse() {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (topTilemap.WorldToCell(mouseWorldPos), new Vector2(mouseWorldPos.x, mouseWorldPos.y));
    }
}
