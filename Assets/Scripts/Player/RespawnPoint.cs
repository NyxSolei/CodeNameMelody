using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private string _playerTag = "Player";
    private bool _isSet = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(this._playerTag) && !this._isSet)
        {
            PlayerControls.instance.SetCheckpoint(this.transform.position.x, this.transform.position.y);
            SoundSystem.instance.PlaySetCheckpoint();
            this._isSet = !this._isSet;
        }
       
    }
}
