﻿using System;
using UnityEngine;
namespace FrameWorkDesign.Example
{
    public class Game : MonoBehaviour
    {
        private void Awake()
        {
            GameStartEvent.Register(OnGameStart);
        }

        private void OnGameStart()
        {
            transform.Find("Enemies").gameObject.SetActive(true);
        }
        private void OnDestroy()
        {
            GameStartEvent.UnRegister(OnGameStart);
        }
    }
}