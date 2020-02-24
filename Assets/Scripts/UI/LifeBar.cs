using Project.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class LifeBar : MonoBehaviour
    {
        public Slider slider;

        public void SetMaxLifeBar(int life)
        {
            slider.maxValue = life;
            slider.value = life;
        }

        public void SetLifeBar(int life)
        {
            slider.value = life;

        }

    }
}

