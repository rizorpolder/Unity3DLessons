
using UnityEngine;

namespace FPS
{
    public class CameraController : BaseController
    {
        public Camera camera; // obj
        public Transform target;
        public LayerMask obstacles;
        public LayerMask noPlayer;


        public float speedX = 360f;
        public float speedY = 240f;
        public float LimitY = 40f;

        public float minDistance = 1.5f;
        public float hideDistance = 2f;

        private float _maxZoomDistance;
        private float _maxDistance;
        private Vector3 _localPosition;
        private Vector3 _defLocPos;
        private float _currentYRotation;
        private LayerMask _cameOrigin;
        private Vector3 _offset;

        private Vector3 _positon
        {
            get { return camera.transform.position; }
            set { camera.transform.position = value; }
        }


        public CameraController(Camera cam, Transform targ)
        {
            obstacles = LayerMask.GetMask("Map", "Ground");
            noPlayer = LayerMask.GetMask("Water", "TransparentFX", "UI", "IgnoreRaycast", "Map", "Ground");
            this.target = targ.transform.Find("FocusTarget").gameObject.transform;
            this.camera = cam;
            _cameOrigin = camera.cullingMask;

            _localPosition =
                target.InverseTransformPoint(
                    _positon); // перевели позицию камеры в локальную систему координат таргета (записали в локал позишн) из мировых
            _defLocPos = target.TransformPoint(_localPosition); // сохраняет дефолтное положение камеры


            _maxDistance = Vector3.Distance(_positon, target.position);
            _maxZoomDistance = _maxDistance * 2;

        }

        public override void MyUpdate()
        {
            _positon = target.TransformPoint(
                _localPosition); //присвоили камере положение локальной позиции (относительно цели) в глобальной системе координат

            CameraRotation(Input.GetKey(KeyCode.Mouse1), Input.GetKeyUp(KeyCode.Mouse1), Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"));
            ObstaclesReact();
            PlayerReact();

            _localPosition =
                target.InverseTransformPoint(
                    _positon); // перевели позицию камеры в локальную систему координат относительно таргета (записали в локал позишн) из мировых

            //Debug.Log($"LocalPos{_localPosition} ,Pos{_positon}");


        }


        //private void CameraZoom(float zoom)
        //{
        //    if (zoom != 0)
        //    {
        //        var temp = target.TransformPoint(_localPosition);
        //        temp.z += zoom;
        //        _localPosition = temp;
        //    }
        //}



        /// <summary>
        /// функционал поворота и возврата
        /// </summary>
        /// <param name="isDown">Нажата ли кнопка</param>
        /// <param name ="isUp"> Отпущена ли кнопка</param>
        /// <param name="mx">Значение Х</param>
        /// <param name="my">Значение У</param>
        private void CameraRotation(bool isDown, bool isUp, float mx, float my)
        {

            if (isDown)
            {
                if (my != 0)
                {
                    var tmp = Mathf.Clamp(_currentYRotation + my * speedY * Time.deltaTime * -1, -LimitY, LimitY);
                    if (tmp != _currentYRotation)
                    {
                        var rot = tmp - _currentYRotation;
                        camera.transform.RotateAround(target.position, camera.transform.right, rot);
                        _currentYRotation = tmp;
                    }
                }

                if (mx != 0)
                {
                    camera.transform.RotateAround(target.position, Vector3.up, mx * speedX * Time.deltaTime);

                }
            }

            ///Сброс камеры в дефолт
            //if (isUp)
            //{
            //    _positon = target.TransformPoint(_defLocPos);
            //    _currentYRotation = 0;
            //    _localPosition = target.InverseTransformPoint(_positon);
            //}

            //camera.transform.LookAt(target);
        }


        /// <summary>
        /// При приближении к стене - камера приближается к игроку, а не проваливается в текстуру
        /// </summary>
        private void ObstaclesReact()
        {
            var distance = Vector3.Distance(_positon, target.position);
            RaycastHit hit;
            if (Physics.Raycast(target.position, camera.transform.position - target.position, out hit, _maxDistance,
                obstacles))
            {
                _positon = hit.point;
            }
            else if (distance < _maxDistance && !Physics.Raycast(_positon, -camera.transform.forward, .1f, obstacles))
            {
                _positon -= camera.transform.forward * 0.05f;
            }
        }

        /// <summary>
        /// Отключение отрисовки игрока при макс.приближении
        /// </summary>
        private void PlayerReact()
        {
            var distance = Vector3.Distance(_positon, target.position);
            if (distance < hideDistance)
                camera.cullingMask = noPlayer;
            else
                camera.cullingMask = _cameOrigin;
        }
    }
}
