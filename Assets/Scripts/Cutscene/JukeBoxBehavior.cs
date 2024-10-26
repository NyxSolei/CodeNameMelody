using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxBehavior : MonoBehaviour
{
    private bool _hasFirstCutscenePlayed = false;
    LayerMask whatIDamage;
    private void SetLayerForCheck(LayerMask layer)
    {
        this.whatIDamage.value = 64;
    }

    public bool IsInLayer(GameObject gameObject)
    {
        this.SetLayerForCheck(gameObject.layer);
        return (this.whatIDamage.value & (1 << gameObject.layer)) != 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (IsInLayer(collision.gameObject))
        {
            if (this._hasFirstCutscenePlayed && WinningBehavior.instance.WinConditionsCheck())
            {
                CutsceneManager.instance.StartFinalCutscene();
            }
            else if(!this._hasFirstCutscenePlayed)
            {
                CutsceneManager.instance.StartFirstJukeCutscene();
                _hasFirstCutscenePlayed = true;
            }
        }
    }
}
