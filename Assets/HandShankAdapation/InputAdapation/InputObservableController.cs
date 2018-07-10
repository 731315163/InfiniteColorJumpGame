using System.Collections.Generic;
using GDGeek;
using UniRx;
using UnityEngine;
using E = UnityEngineExtension;
using Input = UnityEngine.Input;

namespace Assets.HandShankAdapation.InputAdapation
{
    public class InputObservableController : Singleton<InputObservableController> {
        

        public IObservable<KeyCode> KeyDownStream { get; set; }
        public IObservable<KeyCode> KeyUpStream { get; set; }
        public IObservable<KeyCode> KeyHoldStream { get; set; }
        private readonly IObservable<KeyCode> listenKeyCodes = Key.KeyCodes.ToObservable();

        public IObservable<IList<Timestamped<KeyCode>>> LeftArrowTimeSpan { get; set; }
        public IObservable<IList<Timestamped<KeyCode>>> RightArrowTimeSpan { get; set; }

        void Awake ()
        {
            InitKeyDownStream();
            InitKeyUpStream();
            InitKeyHoldStream();
            LeftArrow();
            RightArrow();
        }

        protected void InitKeyDownStream()
        {
            KeyDownStream = Observable.EveryUpdate()
                .Where
                    (_ => Input.anyKeyDown)
                .SelectMany
                    (_ => listenKeyCodes)
                .Where
                    (key => Input.GetKeyDown(key) || Input.GetKey(key))
                .Do
                    (_ => E.Input.IsAnyKeyHoldDown = true);

        }

        protected void InitKeyHoldStream()
        {
            KeyHoldStream = Observable.EveryUpdate()
                .Where
                    (_ => Input.anyKey)
                .SelectMany
                    (_ => listenKeyCodes)
                .Where
                    (key => Input.GetKey(key));

        }
        protected void InitKeyUpStream()
        {
            KeyUpStream = Observable.EveryUpdate()
                .Where
                    ( _ => E.Input.IsAnyKeyHoldDown)
                .SelectMany
                    ( _ => listenKeyCodes )
                .Where
                    ( key => Input.GetKeyUp(key) )
                .Do
                    ( _ => E.Input.IsAnyKeyHoldDown = false );
          
        }
        
        protected void LeftArrow()
        {
            LeftArrowTimeSpan = FilterStream(KeyHoldStream, Key.LeftArrow).Buffer(2);
        }

        protected void RightArrow()
        {
            RightArrowTimeSpan = FilterStream(KeyHoldStream, Key.RightArrow).Buffer(2);
        }
       
        protected IObservable<Timestamped<KeyCode>> FilterStream(IObservable<KeyCode> stream, KeyCode keycode)
        {
            return stream
                .Where
                    (key => key == keycode)
                .Timestamp();
        }
    }
}
