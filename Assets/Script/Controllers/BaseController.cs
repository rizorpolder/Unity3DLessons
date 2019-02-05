
namespace FPS
{ 
    public abstract class BaseController
    {

        public bool isActive = false;

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(BaseObjectScene obj=null)
        {
            isActive = true;
        }

        public virtual void Off()
        {
            isActive = false;
        }

        public virtual void Switch()
        {
            if (isActive)
            {
                Off();
            }

             else
             {
                 On();
             }


        }

        public abstract  void MyUpdate();

    }
}
