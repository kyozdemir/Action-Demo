using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class CharacterManager : MonoBehaviour
    {
        protected void OnDeath(Character character)
        {
            StartCoroutine(RespawnRoutine(character));
        }

        private IEnumerator RespawnRoutine(Character character)
        {
            yield return new WaitForSeconds(Constants.Prefs.CHARACTER_WAITING_FOR_RESPAWN_DURATION);
            character.Respawn();
        }
    }
}
