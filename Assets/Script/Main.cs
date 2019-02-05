using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class Main : MonoBehaviour
    {
        //public Camera camera;
        public MouseMove mouseMove;
        public PlayerController playerController;
        public CameraController cameraController;
        public Camera MainCamera { get; private set; }
        public Transform player { get; private set; }
        private BaseController[] _controllers;
        public static Main Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            MainCamera = Camera.main;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerController = new PlayerController(new PlayerMove(player));
            cameraController = new CameraController(MainCamera, player);
            cameraController.On();
            _controllers = new BaseController[2]
            {
               playerController,
               cameraController,
            };
        }


        void Update()
        {
            foreach (var controller in _controllers)
            {
                controller.MyUpdate();
                ;
            }


        }
    }
}