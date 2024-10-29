using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] Transform[] _trapLocations;
    [SerializeField] GameObject[] _trapsPrefabs = new GameObject[3];
    private float _addedYToFallingTrap = 20;
    private int _blockTrapIndex = 0;
    private int _fallingTrapIndex = 1;
    private float _addedYToBlockTrap = 8.5f;
    private int _minHealth = 0;

    
    private int _firstIndex = 0;
    
    void Start()
    {
        this.RandomizeTrapsAtStart();
    }

    private void Update()
    {
        if(this.gameObject.transform.childCount > 0)
        {
            foreach (Transform trap in this.gameObject.transform)
            {
                if (this.CheckDestroyConditions(trap.gameObject))
                {
                    DamageInterface.IDamagable damagable = trap.gameObject.GetComponent<DamageInterface.IDamagable>();
                    if (damagable != null)
                    {
                        damagable.Die();
                    }
                }
            }
        }

    }

    private bool CheckDestroyConditions(GameObject trap)
    {
        bool destroy = false;

        // Try checking health from FallingTrapManager
        if (trap.gameObject.GetComponent<FallingTrapManager>() != null)
        {
            destroy = trap.gameObject.GetComponent<FallingTrapManager>().GetHealth() < this._minHealth;
        }

        // Try checking health from GroundTrapManager
        if (trap.gameObject.GetComponent<GroundTrapManager>() != null)
        {
            destroy = trap.gameObject.GetComponent<GroundTrapManager>().GetHealth() < this._minHealth;
        }

        // Try checking health from BlockTrapManager
        if (trap.gameObject.GetComponent<BlockTrapManager>() != null)
        {
            destroy = trap.gameObject.GetComponent<BlockTrapManager>().GetHealth() < this._minHealth;
        }

        return destroy;
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
