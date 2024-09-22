using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player
{
    public class PaddleController : MonoBehaviour
    {
        public bool BallLocked { get; private set; } = true;

        private BallController _ballController;

        private Camera _mainCamera;

        private void Start()
        {
            _ballController = GetComponentInChildren<BallController>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (BallLocked && Input.GetMouseButtonDown(0))
            {
                BallLocked = false;
                _ballController.transform.parent = null;
                _ballController.ReleaseBall();
            }

            Vector3 minScreenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _mainCamera.nearClipPlane));
            Vector3 maxScreenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, _mainCamera.nearClipPlane));


            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y = transform.position.y;
            mousePos.x = Mathf.Clamp(mousePos.x, minScreenBounds.x + (transform.localScale.x / 2),
                maxScreenBounds.x - (transform.localScale.x / 2));
            transform.position = mousePos;
        }
    }
}