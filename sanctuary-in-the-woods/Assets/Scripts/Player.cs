using System;
using System.Collections;
using Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class Player : MonoBehaviour
{
    public float PlayerSpeed = 5.0f;

    private ItemRegistry _itemRegistry;
    private PlayerInventory _playerInventory;
    
    private Rigidbody2D Rigidbody;
    private Vector2 PlayerDirection;
    private Tilemap TopTilemap;
    
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int Interact = Animator.StringToHash("interact");
    private static readonly int WalkBack = Animator.StringToHash("walkBack");
    private bool _lookRight;
    private const float _THRESHOLD = 0.3f;
        
    private GameObject _gameObject;
    
    #region PREFABS

    public GameObject BirdHouse;
    public GameObject BerryPlant;
    #endregion

    private void Start()
    {
        _itemRegistry = FindObjectOfType<ItemRegistry>();
        _playerInventory = FindObjectOfType<PlayerInventory>();
        
        Rigidbody = gameObject.AddComponent<Rigidbody2D>();
        Rigidbody.gravityScale = 0.0f;
        Rigidbody.freezeRotation = true;
        
        _animator = GetComponent<Animator>();
        _gameObject = gameObject;

        TopTilemap = GameObject.Find("Top").GetComponent<Tilemap>();
        
        _playerInventory.Add(new StackedItem(_itemRegistry.SeedFeed, 16)); //TODO: GEHT SOFORT LOS
        _playerInventory.Add(new StackedItem(_itemRegistry.Seeds, 5));
        _playerInventory.Add(new StackedItem(_itemRegistry.BerryFeed, 16));
    }

    private void Movement() {
        PlayerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _animator.SetBool(IsWalking, PlayerDirection != Vector2.zero);
        _animator.SetBool(WalkBack, PlayerDirection.y > _THRESHOLD);
        if (PlayerDirection.x > _THRESHOLD && !_lookRight) {
            Flip();
        }
        else if (PlayerDirection.x < -_THRESHOLD && _lookRight) {
            Flip();
        }
    }

    private IEnumerator DestroyTopTiles() {
        if (!Input.GetMouseButtonDown(0)) yield break;

        _animator.SetTrigger(Interact);
        // wait: for better synchronization with the animation
        yield return new WaitForSeconds(0.3f);
        
        var (tile_pos, world_pos) = TilemapUtils.GetTileOnMouse(TopTilemap);
        var distance = MathF.Abs(Vector2.Distance(world_pos, transform.position));

        if (distance > 2.0f) yield break;
        
        var oldTile = TopTilemap.GetTile(tile_pos);
        if (oldTile == null) yield break;

        var itemRegistry = FindObjectOfType<ItemRegistry>();
        var playerInventory = FindObjectOfType<PlayerInventory>();
        
        playerInventory.Add(new StackedItem(itemRegistry.Weed, 1));
        
        TopTilemap.SetTile(tile_pos, null);
    }

    private IEnumerator PetAnimal() {
        if (!Input.GetMouseButtonDown(0)) yield break;
        // TODO: add animation (3 loops of Interact), behaviour of animal
    }
    
    // Update is called once per frame
    void Update() {
        Movement();
        StartCoroutine(DestroyTopTiles());

        if (Input.GetKeyDown(KeyCode.E))
        {
            var transform = GameObject.Find("Canvas").transform;
            var obj = transform.GetChild(1).gameObject;
            obj.SetActive(!obj.activeSelf);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            HandleObjectPlacement();
        }
    }

    private void FixedUpdate() {
        Rigidbody.velocity = new Vector2(PlayerDirection.x * PlayerSpeed, PlayerDirection.y * PlayerSpeed);
    }
    
    private void Flip() {
        var currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        _gameObject.transform.localScale = currentScale;
        _lookRight = !_lookRight;
    }

    private void HandleObjectPlacement()
    {
        if (HandleSimpleObjectPlacement(new StackedItem(_itemRegistry.BirdHouse), BirdHouse))
        {
            return;
        }
        if (HandleSimpleObjectPlacement(new StackedItem(_itemRegistry.Seeds), BerryPlant))
        {
            return;
        }
    }

    private bool HandleSimpleObjectPlacement(StackedItem stack, GameObject targetObject)
    {
        if (!_playerInventory.CanRemove(stack))
        {
            return false;
        }
        
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!NavMeshUtils.IsAccessible(targetPos))
        {
            return false;
        }

        _playerInventory.Remove(stack);
        Instantiate(targetObject, new Vector3(targetPos.x, targetPos.y, 1.0f), Quaternion.identity);
        
        return true;
    }
}
