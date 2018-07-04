using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HandShankAdapation.UIBounds
{
   public abstract class MaskUI:UIRect
    {
        protected MaskUI(Behaviour go) : base(go)
        {
        }
    }
}
