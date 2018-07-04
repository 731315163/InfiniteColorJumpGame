using System;
using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;
using Assets.HandShankAdapation.Messenger;
using GDGeek;
using GDGeek.Pro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HandShankAdapation
{
    public class Tutorial : MonoBehaviour
    {
        public enum StateCtrl
        {
             Empty,
            OneEntry,
            TowEntry,
            SelectPower,
            SelectTower,
            EntryUp,
            EntryDown,
            
        }
        public FSM Fsm = new FSM();
        public GameObject TutorialContainer;
        public Text ShowTutorial;

        protected KeyCode Entry = 
#if UNITY_EDITOR
            KeyCode.Space;
#else
           KeyCode.JoystickButton0;
#endif
        
        public string[] TutorialText;

        void Awake()
        {
            int attack = PlayerPrefs.GetInt("TutorialAttack");
            int skill = PlayerPrefs.GetInt("TutorialSkill");
            
            if (attack == 1 && skill == 1) 
                Destroy(this.gameObject);
            
            Fsm.addState(Empty());
            Fsm.addState(GetEntryState(StateCtrl.OneEntry, 0, StateCtrl.TowEntry));
            State AttackTower = GetEntryState(StateCtrl.TowEntry, 1, StateCtrl.Empty);
            AttackTower.onOver += delegate { PlayerPrefs.SetInt("TutorialAttack", 1); };
            Fsm.addState(AttackTower);

            Fsm.addState(GetEntryState(StateCtrl.SelectPower, 2, StateCtrl.SelectTower));
            Fsm.addState(OverState(3));
            
            Fsm.init(attack != 1 ? StateCtrl.OneEntry.ToString() : StateCtrl.Empty.ToString());
            MessageManager.Instance.AddListener<IInputManager<InputOP>>(Entry.ToString(), ChangeState);
        }
       
        protected void ChangeState(IInputManager<InputOP> input)
        {
           
            
            var key = input.GetInput();
            
            if(key.InputState == (int)KeyState.Down)
                Fsm.post(StateCtrl.EntryDown.ToString());

        }

        protected State Empty()
        {
            var state = new State(StateCtrl.Empty.ToString());
            state.onStart += delegate { TutorialContainer.hide(); };
            return state;
        }

        protected State GetEntryState(ValueType name,int screenshow,ValueType nextstatename)
        {
            var state = new State(name.ToString());
            state.onStart += delegate
            {
                TutorialContainer.show();
                ShowTutorial.text = TutorialText[screenshow];
            };
            state.addAction(StateCtrl.EntryDown.ToString(),nextstatename.ToString());
            return state;
        }
       
        protected State OverState(int index)
        {
            var jump = new State(StateCtrl.SelectTower.ToString());
            jump.onStart += delegate { ShowTutorial.text = TutorialText[index]; };
            jump.onOver += delegate
            {
                 PlayerPrefs.SetInt("TutorialSkill", 1);
                Destroy(this.gameObject);
            };
            jump.addAction(StateCtrl.EntryDown.ToString(),StateCtrl.Empty.ToString());
            return jump;
        }
    }
}
