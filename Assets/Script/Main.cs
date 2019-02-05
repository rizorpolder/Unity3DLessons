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
        public Transform player { get; private set; }
        private BaseController[] _controllers;
        public static Main Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            //camera = Camera.main;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerController = new PlayerController(new PlayerMove(player));
            //playerController = new PlayerController(new MouseMove(player));
            _controllers = new BaseController[1]
            {
               playerController,
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