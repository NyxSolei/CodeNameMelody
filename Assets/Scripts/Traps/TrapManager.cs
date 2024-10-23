using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] Transform[] _trapLocations;
    [SerializeField] GameObject[] _trapsPrefabs = new GameObject[3];
    private float _addedYToFallingTrap = 35;
    private int _blockTrapIndex = 0;
    private int _fallingTrapIndex = 1;
    private float _addedYToBlockTrap = 5.5f;

    
    private int _firstIndex = 0;
    
    void Start()
    {
        this.RandomizeTrapsAtStart();
    }



    private void RandomizeTrapsAtStart()
    {
        foreach(Transform loc in this._trapLocations)
        {
            int instantiationIndex = Random.Range(this._firstIndex, this._trapsPrefabs.Length);
            GameObject trapToInstantiate = this._trapsPrefabs[instantiationIndex];
            GameObject instantiatedTrap = Instantiate(trapToInstantiate, this.GetComponent<Transform>());
            Transform trapLocation = instantiatedTrap.GetComponent<Transform>();

            float yCreationPosition = loc.position.y;

            if(instantiationIndex == this._blockTrapIndex)
            {
                yCreationPosition += this._addedYToBlockTrap;
            }
            else if(instantiationIndex == this._fallingTrapIndex)
            {
                yCreationPosition += this._addedYToFallingTrap;
            }

            trapLocation.position = new Vector2(loc.position.x, yCreationPosition);

            
        }
    }
}
