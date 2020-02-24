using Project.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.SpellSystem
{
    [System.Serializable]
    public class SpellBook
    {
        public PlayerManager player;
        public List<Spell> allSpells;
        public List<Spell> learnSpells;

        public Spell basicSpell;
        public Spell heavySpell;
        public Spell buffSpell;
        public Spell crowdControlSpell;


        public SpellBook(PlayerManager _player)
        {
            this.player = _player;
        }

        public void LearnSpell(Spell spell)
        {
            this.learnSpells.Add(spell);
        }

        public Spell GetSpellSelected()
        {
            Spell spellSelected = null;

            if (Input.GetKeyDown(this.player.basicSpellInput))
            {
                spellSelected = this.basicSpell;
            }
            else if (Input.GetKeyDown(this.player.heavySpellInput))
            {
                spellSelected = this.heavySpell;
            }
            else if (Input.GetKeyDown(this.player.buffSpellInput))
            {
                spellSelected = this.buffSpell;
            }
            else if (Input.GetKeyDown(this.player.crowdControlSpellInput))
            {
                spellSelected = this.crowdControlSpell;
            }
            return spellSelected;
        }
    }
}

