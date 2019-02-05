using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FPS

{
    public class BaseObjectScene:MonoBehaviour
    {

        private int _layer;
        private Color _color;
        private bool _isVisible;

        [HideInInspector] Rigidbody rigidbody;

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        #region Properties

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;
                AskLayer(transform, value);
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                AskColor(transform, value);
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                var tempRenderer = GetComponent<Renderer>();
                if (tempRenderer)
                    tempRenderer.enabled = _isVisible;
                if (transform.childCount <= 0) return;
                foreach (Transform d in transform)
                {
                    tempRenderer = d.gameObject.GetComponent<Renderer>();
                    if (tempRenderer) tempRenderer.enabled = _isVisible;

                }
            }
        }
        #endregion

        #region Functions

        private void AskLayer(Transform obj, int lvl)
        {
            gameObject.layer = lvl;
            if(obj.childCount<=0)return;
            foreach (Transform transform in obj)
            {
                AskLayer(transform,lvl);
            }
        }

        private void AskColor(Transform obj, Color color)
        {
            foreach (var curMaterial in obj.GetComponent<Renderer>().materials)
            {
                curMaterial.color = color;
            }

            if (obj.childCount <= 0) return;
            foreach (Transform transform in obj)
            {
                AskColor(transform, color);
            }

        }

        private bool IsRigidbody()
        {
            return rigidbody;
        }

        private void DisableRigitBody()
        {
            if (!IsRigidbody()) return;
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
        }
        private void EnableRigitBody()
        {
            if (!IsRigidbody()) return;
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
            }
        }

        private void EnableRigitBody(float force)
        {
            EnableRigitBody();
            rigidbody.AddForce(transform.forward*force);
        }

        public void SetActive(bool value)
        {
            IsVisible = value;

            var tempCollider = GetComponent<Collider>();
            if (tempCollider)
            {
                tempCollider.enabled = value;
            }
        }

        protected void MyInvoke(Action method, float time)
        {
            Invoke(method.Method.Name, time);
        }

        protected void MyCancelInvoke(Action method)
        {
            CancelInvoke(method.Method.Name);
        }

        protected void MyInvokeRepeating(Action method, float time, float repeatRate)
        {
            InvokeRepeating(method.Method.Name, time, repeatRate);
        }

        protected virtual void OnDisable()
        {
            CancelInvoke();
        }

        #endregion
    }
}
