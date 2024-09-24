using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class PowerupsUITimer : MonoBehaviour
    {
        [SerializeField] private Image _iconBG;
        
        [SerializeField]private float _timer;
        [SerializeField]private float _timerStartValue;

        [SerializeField]private bool _start;

        private void Update()
        {
            if (!_start) return;
            
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                _iconBG.fillAmount = _timer / _timerStartValue;
            }
        }

        public void Setup(float timer)
        {
            _timer = timer;
            _timerStartValue = timer;
            _start = true;
        }

    }
}