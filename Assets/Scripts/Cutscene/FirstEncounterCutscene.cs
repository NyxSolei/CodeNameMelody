using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEncounterCutscene : MonoBehaviour
{
    private bool _hasPlayed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_hasPlayed)
        {
            _hasPlayed = true;
            CutsceneManager.instance.StartFirstEncounterCutscene();
        }
    }
}
