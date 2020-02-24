using Project.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    //private PlayerManager player = PlayerManager.Instance;
    public Slider slider;

    public void SetMaxManaBar(int mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetManaBar(int mana)
    {
        slider.value = mana;
    }
}
