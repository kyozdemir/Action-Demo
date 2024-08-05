using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class PlayerManager : CharacterManager
    {
        [SerializeField] private PlayerController currentPlayer;

        private void Start()
        {
            currentPlayer.Initialize();
            currentPlayer.OnDeaht += OnDeath;
        }
    }
}
