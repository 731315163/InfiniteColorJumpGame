using System.Collections.Generic;
using Assets.HandShankAdapation.InputAdapation;
using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;
using Assets.HandShankAdapation.Messenger;
using GDGeek;
using UnityEngine;

namespace Assets.HandShankAdapation.UGUI.AdapationStrategy
{
    public class Strategy
    {
        public static readonly string 
            ChangeStrategy = "ChangeStrategy",
            DefaultStrategy = "DefaultStrategy",
            GameStragegy = "GameStragegy",
            ShopStrategy = "ShopStrategy";
    }
    public class OperateModeManager : Singleton<OperateModeManager>
    {
        
        protected KeyPressStrategy CurrentKeyPressStrategy;

        protected KeyCode UpKey = 
#if UNITY_EDITOR
             KeyCode.W;
#else
            KeyCode.UpArrow;
#endif

        protected KeyCode DownKey =
#if UNITY_EDITOR
        KeyCode.S;
#else
            KeyCode.DownArrow;
#endif

        protected KeyCode LeftKey=
#if UNITY_EDITOR
            KeyCode.A;
#else
            KeyCode.LeftArrow;
#endif

        protected KeyCode RightKey=
#if UNITY_EDITOR
            KeyCode.D;
#else
            KeyCode.RightArrow;
#endif

        protected KeyCode EntryKey=
#if UNITY_EDITOR
            KeyCode.Space;
#else
            KeyCode.JoystickButton0;
#endif

        protected KeyCode ESCKey = KeyCode.Escape;

        protected FSM fsm;
        public string CurrentStrategy;
        void Awake()
        {
            fsm = new FSM();
             foreach (var strategy in this.GetComponents<KeyPressStrategy>())
             {
                 fsm.addState(strategy.Name,strategy.StrategyInit());
             }
            AddKeyEventListener();
           ChangeStrategy(CurrentStrategy);
            
        }

       protected void AddKeyEventListener()
        {
            MessageManager.Instance.AddListener<string>("ChangeStrategy",ChangeStrategy);
            MessageManager.Instance.AddListener<IInputManager<InputOP>>(UpKey.ToString(),UpEvent);
            MessageManager.Instance.AddListener<IInputManager<InputOP>>(DownKey.ToString(),DownEvent);
            MessageManager.Instance.AddListener<IInputManager<InputOP>>(LeftKey.ToString(),LeftEvent);
            MessageManager.Instance.AddListener<IInputManager<InputOP>>(RightKey.ToString(),RightEvent);
            MessageManager.Instance.AddListener<IInputManager<InputOP>>(EntryKey.ToString(),EntryEvent);
            MessageManager.Instance.AddListener<IInputManager<InputOP>>(ESCKey.ToString(),EscEvent);
        }
        

        public void ChangeStrategy(string strategyname)
        {
            CurrentStrategy = strategyname;
            fsm.translation(strategyname);
            CurrentKeyPressStrategy = (fsm.getCurrSubState() as StrategyState).Strategy;
        }

        public void UpEvent(IInputManager<InputOP> input)
        {
            var operate = CurrentKeyPressStrategy as IUpArrowOperate;
            if(operate != null)operate.UpArrowOperate(input);

        }
        public void DownEvent(IInputManager<InputOP> input)
        {
            var operate = CurrentKeyPressStrategy as IDownArrowOperate;
            if(operate != null)operate.DownArrowOperate(input);
        }
        public void LeftEvent(IInputManager<InputOP> input)
        {
            var operate = CurrentKeyPressStrategy as ILeftArrowOperate;
            if(operate != null)operate.LeftArrowOperate(input);
        }
        public void RightEvent(IInputManager<InputOP> input)
        {
            var operate = CurrentKeyPressStrategy as IRigthtArrowOperate;
            if(operate != null)operate.RightArrowOperate(input);
        }
       
        public void EntryEvent(IInputManager<InputOP> input)
        {
            var operate = CurrentKeyPressStrategy as IEnterOperate;
            if(operate != null)operate.EnterOperate(input);
        }
        public void EscEvent(IInputManager<InputOP> input)
        {
            var operate = CurrentKeyPressStrategy as IEscOperate;
            if(operate != null)operate.EscOperate(input);
        }

    }
}
