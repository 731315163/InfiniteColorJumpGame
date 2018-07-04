using System.Collections;
using System.Collections.Generic;
using Assets.HandShankAdapation.CodeModel;
using Assets.HandShankAdapation.Interactivity.UGUI;
using Assets.HandShankAdapation.UGUI;
using Assets.HandShankAdapation.UGUI.AdapationStrategy;
using UnityEngine;

public class ChangeShopStrage : ChangeState
{

    public Behaviour m;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (m.enabled && OperateModeManager.Instance.CurrentStrategy != Strategy.ShopStrategy)
	    {
	        FindInteractivityUI.Instance.SetRoot(m);
	        Change(Strategy.ShopStrategy);
	    }
        else if( ! m.enabled && OperateModeManager.Instance.CurrentStrategy  != Strategy.DefaultStrategy)
            Change(Strategy.DefaultStrategy);

            
	}
}
