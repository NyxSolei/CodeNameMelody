using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] Transform[] _trapLocations;
    [SerializeField] GameObject[] _trapsPrefabs = new GameObject[3];


    
    private int _firstIndex = 0;
    
    void Start()
    {
        this.RandomizeTrapsAtStart();
    }



    private void RandomizeTrapsAtStart()
    {
        foreach(Transform loc in this._trapLocations)
        {
            GameObject trapToInstantiate = this._trapsPrefabs[Random.Range(this._firstIndex, this._trapsPrefabs.Length)];
            GameObject instantiatedTrap = Instantiate(trapToInstantiate);
            Transform trapLocation = instantiatedTrap.GetComponent<Transform>();
            trapLocation.position = new Vector2(loc.position.x, loc.position.y);

            // if it's a falling trap type, we also need to set the trigger area
            // TODO!
        }
    }
}
