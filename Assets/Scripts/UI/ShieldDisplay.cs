using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDisplay : MonoBehaviour
{
    [SerializeField] GameObject[] _shieldPrefabs;
    private GameObject[] _instantiatedShieldPrefabs = new GameObject[5];
    private int _imageRemovalIncrement = 2;

    public static ShieldDisplay instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }


    public void InstantiateNewShieldDisplay()
    {
        foreach(GameObject prefab in this._shieldPrefabs)
        {
            this._instantiatedShieldPrefabs[System.Array.IndexOf(this._shieldPrefabs, prefab)] = Instantiate(prefab, this.transform);
        }
    }

    public void UpdateShieldDisplay()
    {
        /*int lengthOfArray = PlayerControlPiano.instance.GetCurrentShieldPower()/this._imageRemovalIncrement;

        if (lengthOfArray < this._instantiatedShieldPrefabs.Length && this._instantiatedShieldPrefabs!=null)
        {
            for(int removalIndex=lengthOfArray; removalIndex<this._instantiatedShieldPrefabs.Length; removalIndex++)
            {
                Destroy(this._instantiatedShieldPrefabs[removalIndex]);
            }
        }
        */
        int currentShieldPower = PlayerControls.instance.GetShieldPower();
        int shieldsToKeep = currentShieldPower / _imageRemovalIncrement;

        // Destroy only the shields that are beyond the current shield power
        for (int i = shieldsToKeep; i < _instantiatedShieldPrefabs.Length; i++)
        {
            if (_instantiatedShieldPrefabs[i] != null)
            {
                Destroy(_instantiatedShieldPrefabs[i]);
                _instantiatedShieldPrefabs[i] = null;  // Nullify to avoid future issues
            }
        }
    }
}
