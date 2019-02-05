
using UnityEngine;
using UnityEngine.Experimental.UIElements;


namespace FPS
{
    public class WeaponController:BaseController
    {
        private Weapon _weapon;
        private int _mouseButton = (int) MouseButton.LeftMouse;

        public override void MyUpdate()
        {
            if (!isActive) return;
            if (Input.GetMouseButton(_mouseButton))
            {
               _weapon.Fire();
            }
        }

        public override void On(BaseObjectScene weapon)
        {
            if (isActive) return;
            base.On(weapon);
            _weapon = weapon as Weapon;
            _weapon.IsVisible = true;
        }

        public override void Off()
        {
            if (!isActive) return;
            base.Off();
            _weapon.IsVisible = false;
            _weapon = null;
          }
    }
}
