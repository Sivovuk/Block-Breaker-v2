using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class PowerupsTimerDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject _powerupMulty;
        [SerializeField] private GameObject _powerupPaddleExtend;
        [SerializeField] private GameObject _powerupFireball;
        [SerializeField] private GameObject _powerupGun;
        [SerializeField] private Transform _parent;

        public static PowerupsTimerDisplay Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddPowerTimer(int index, float duration, CollectableType type)
        {
            GameObject prefab = null;
            switch (type)
            {
                case CollectableType.Multiply :
                    prefab = _powerupMulty;
                    break;
                case CollectableType.PaddleExtend :
                    prefab = _powerupPaddleExtend;
                    break;
                case CollectableType.Fireball :
                    prefab = _powerupFireball;
                    break;
                case CollectableType.Gun :
                    prefab = _powerupGun;
                    break;
            }
            
            GameObject spawn = Instantiate(prefab, _parent);
            spawn.transform.localScale = new Vector3(1, 1, 1);
            spawn.GetComponent<PowerupsUITimer>().Setup(duration);
        }
    }
}