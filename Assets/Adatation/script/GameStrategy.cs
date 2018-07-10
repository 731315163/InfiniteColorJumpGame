using System;
using System.Collections.Generic;
using Assets.HandShankAdapation.InputAdapation;
using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;
using GDGeek;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Adatation.script
{
    public class GameStrategy:KeyPressStrategy
    {
        protected GameObject Player;
        
        protected List<IDisposable> InputStream = new List<IDisposable>();
        public UnityEvent PauseEvent;

        void Start()
        {
            Player = GameObject.Find("PlayerBall");
        }
        public override void OnStart()
        {
            var stream = InputObservableController.Instance.KeyDownStream
                .Where(key => key == Key.Entry)
                .Subscribe(_=>{Player.SendMessage("Jump");});
            InputStream.Add(stream);

             stream = InputObservableController.Instance.KeyDownStream
           .Where(key => key == Key.Esc)
           .Subscribe(_ =>
                 {
                     PauseEvent.Invoke();
                 });
            InputStream.Add(stream);
        }

        protected void Jump(KeyCode key)
        {
            if(Player != null)
                Player.SendMessage("Jump");
        }
        
        public override void OnOver()
        {
            InputStream.ForEach(dispose => dispose.Dispose());
        }
    }
}