using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FPS
{
    public class MouseMove :IMove
    {
        private Camera _camera;
        private Transform _instance;
        private Ray _ray { get;  set; }
        private RaycastHit _hit;

        private bool IsMoving { get; set; }
        public float speed = 0.1f;
        public float radius = 0.2f;
        public Vector3 offset = new Vector3(0, 1.5f, 0);

        public MouseMove(Transform obj)
        {
            _instance = obj;
            _camera = Camera.main;
        }



        public void Move()
        {
            MouseClick(Input.GetMouseButton(1));
            
        }

        private void MouseClick(bool pressed)
        {
            Debug.Log($"Pos {Input.mousePosition}");
           _ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                if (_hit.transform.tag == "Ground")
                {
                    CharacterMove(_hit.point);
                }
            }
        }

        private void CharacterMove(Vector3 point)
        {
            IsMoving = true;
            _instance.LookAt(point+offset);
            if (IsMoving)
            {
                _instance.position = _instance.position + _instance.forward * speed * Time.deltaTime;
                if (Vector3.Distance(_instance.position, point + offset) < radius)
                {
                    IsMoving = false;
                }
            }
        }
    }
}
