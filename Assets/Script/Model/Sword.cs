using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPS;

namespace Assets.Script.Model
{
   public class Sword:Weapon
    {
        public override void Fire()
        {
            if (!_isReady) return;
            //логика удара мечом
        }
    }
}
