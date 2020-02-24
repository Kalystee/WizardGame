using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.SpellSystem
{
    public abstract class Spell : ScriptableObject
    {
        public string name;
        public string description;
        //public Sprite icon;
        public int manaCost;
        public float castingTime;
        public GameObject vfx;
        public SpellType type;
        //public abstract void Initialize(GameObject obj);

        public abstract void TriggerSpell(IDamageable damageable);
        
    }

    public enum SpellType
    {
        PROJECTILE,
        CIRCLE_AREA
    }
}

