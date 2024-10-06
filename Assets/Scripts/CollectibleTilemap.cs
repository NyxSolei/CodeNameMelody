using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectibleTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase collectibleTile;

    // Start is called before the first frame update
    void Start()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 playerPosition = collision.transform.position;

            Vector3Int gridPosition = tilemap.WorldToCell((Vector3)playerPosition);

            TileBase tileAtPosition = tilemap.GetTile(gridPosition);

            if (tileAtPosition == collectibleTile)
            {
                tilemap.SetTile(gridPosition, null);

                Debug.Log("Collectible picked up!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
