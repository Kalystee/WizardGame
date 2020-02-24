using Cinemachine;
using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    [System.Serializable]
    public class PlayerStats 
    {
        public int maxHealth;
        public int currentHealth;
        public int maxMana;
        public int currentMana;

        public PlayerStats(int maxHealth, int maxMana)
        {
            this.maxHealth = maxHealth;
            this.maxMana = maxMana;
            this.currentHealth = this.maxHealth;
            this.currentMana = this.maxMana;
        }

        public void UseMana(int ammount)
        {
            this.currentMana -= ammount;
        }

        public void RegenMana(int ammount)
        {
            this.currentMana += ammount;
        }

        public void ReduceLife(int ammount)
        {
            this.currentHealth -= ammount;
        }
    }
}

