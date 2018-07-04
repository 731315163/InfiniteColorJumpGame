using Assets.HandShankAdapation.InputHandle;
using Assets.HandShankAdapation.InputHandle.InputManager;

namespace Assets.HandShankAdapation.InputAdapation
{
    public interface ILeftArrowOperate
    {
        void LeftArrowOperate(IInputManager<InputOP> inputmanager);
        
    }

    public interface IRigthtArrowOperate
    {
        void RightArrowOperate(IInputManager<InputOP> inputmanager);
        
    }

    public interface IDownArrowOperate
    {
        void DownArrowOperate(IInputManager<InputOP> inputmanager);
       
    }

    public interface IUpArrowOperate
    {
        void UpArrowOperate(IInputManager<InputOP> inputmanager);
       
    }

    public interface IEnterOperate
    {
        void EnterOperate(IInputManager<InputOP> inputmanager);
       
    }

    public interface IEscOperate
    {
        void EscOperate(IInputManager<InputOP> inputmanager);
        
    }
}