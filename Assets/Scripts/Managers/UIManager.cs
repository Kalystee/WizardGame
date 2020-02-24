using Project.Player;
using Project.UI;
using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Manager
{
    public class UIManager: Singleton<UIManager>
    {
        [Header("Bars")]
        public LifeBar lifeBar;
        public ManaBar manaBar;

    }
}

