using System.Collections.Generic;

namespace Assets.HandShankAdapation.InputHandle.InputManager
{
    public interface IInputManager<T>
    {

        bool IsAnyInput { get; }

        /// <summary>
        /// 获取最后的一个输入并清除这个输入
        /// </summary>
        /// <returns></returns>
        T Used();
        /// <summary>
        /// 获取最早的一个输入并清除这个输入
        /// </summary>
        /// <returns></returns>
        T ReverseUsed();
        IEnumerable<T> UsedAll();
        IEnumerable<T> ReverseUsedAll();
        /// <summary>
        /// 获取最后的输入
        /// </summary>
        /// <returns></returns>
        T GetInput();
        /// <summary>
        /// 获取最早的输入
        /// </summary>
        /// <returns></returns>
        T GetReverseInput();
        IEnumerable<T> GetInputs();
        IEnumerable<T> GetReverseInputs();

    }
}
