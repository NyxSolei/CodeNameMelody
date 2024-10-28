using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BQController : MonoBehaviour
{
    private Vector2 length;
    private Vector2 startPos;
    public GameObject cam;
    public Vector2 parallaxEffect;
    private float _distance;
    public int layerIndex;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceX = cam.transform.position.x * parallaxEffect.x;
        float movementX = cam.transform.position.x * (1 - parallaxEffect.x);

        float distanceY = cam.transform.position.y * parallaxEffect.y;
        float movementY = cam.transform.position.y * (1 - parallaxEffect.y);

        transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, transform.position.z);

        if(movementX > startPos.x + length.x)
        {
            startPos.x += length.x;
        }
        else if (movementX < startPos.x - length.x)
        {
            startPos.x -= length.x;
        }

        if (layerIndex != 1 && layerIndex != 2) 
        {
            if (movementY > startPos.y + length.y)
            {
                startPos.y += length.y;
            }
            else if (movementY < startPos.y - length.y)
            {
                startPos.y -= length.y;
            }
        }
    }

}
