using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace FPS
{
   public abstract class Weapon : BaseObjectScene
    {
        protected Transform transform;
        protected float force = 999;
        protected float _reloadTime = 0.2f;

        protected bool _isReady = true;

        public abstract void Fire();

        public void IsReady()
        {
            _isReady = true;
        }

    }
}
