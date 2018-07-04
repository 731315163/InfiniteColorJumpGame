using System;
using Assets.HandShankAdapation.InputAdapation;
using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;
using GDGeek;
using UniRx;
using UnityEngine;

namespace Assets.Adatation.script
{
    public class GameStrategy:KeyPressStrategy
    {
        public GameObject Player;
        
        protected IDisposable InputStream;

        void Start()
        {
            Player = GameObject.Find("PlayerBall");
        }
        public override void OnStart()
        {
            InputStream = InputObservableController.Instance.KeyDownStream
                .Where(key => key == Key.Entry)
                .Subscribe(Jump);
        }

        protected void Jump(KeyCode key)
        {
            if(Player != null)
                Player.SendMessage("Jump");
        }
        public override void OnOver()
        {
           InputStream.Dispose();
        }
    }
}