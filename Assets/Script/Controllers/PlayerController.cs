using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS

{
    public class PlayerController : BaseController
    {
     private IMove _unit;


     public PlayerController(IMove move)
     {
            _unit = move;
     }

       
        public override void MyUpdate()
        {
            _unit.Move();
        }
    }
}
