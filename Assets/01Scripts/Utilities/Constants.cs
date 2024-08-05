using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public static class Constants
    {
        public static class Animations
        {
            public const string LOCOMOTION_X = "InputX";
            public const string LOCOMOTION_Y = "InputY";
            public const string IS_DEATH = "isDeath";
        }

        public static class PoolKeys
        {
            public const string DAMAGE_NUMBERS = "DamageNumbers";
            public const string COLLECTABLE = "Collectable";
            public const string ENEMY = "Enemy";
        }

        public static class Prefs
        {
            public const float CHARACTER_WAITING_FOR_RESPAWN_DURATION = 3f;
            public const string NAMESPACE_PREFIX = "ActionDemo.";
        }

        public static class Tags
        {
            public const string COLLECTABLE = "Collectable";
            public const string PLAYER = "Player";
        }
    }

    public enum AttachmentType
    {
        ArmorPiercing = 0,
        Barrel = 1,
        Scope = 2
    }

    public enum DamageTypes
    {
        Explosion = 0,
        Impact = 1
    }

    public enum EnemyStates
    {
        Idle = 0,
        Patrol = 1,
        Chase = 2,
        Die = 3
    }

    public enum StatsType
    {
        Armor = 0,
        Health = 1
    }
}
