using Assets.HandShankAdapation.UIBounds;
using UnityEngine;

namespace Assets.HandShankAdapation.UGUI
{
    public interface IFindUIRect
    {
        UIRect Find(Behaviour component);
    }
}
