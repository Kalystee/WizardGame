using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.SpellSystem
{
    [CreateAssetMenu(fileName = "/Damage", menuName ="Spell/Damage")]
    public  class SpellDamage : Spell
    {
        [Header("Damage Settings")]
        public int damage;

        public override void TriggerSpell(IDamageable damageable)
        {
            damageable.TakeDamage(this.damage);
        }
    }
}

