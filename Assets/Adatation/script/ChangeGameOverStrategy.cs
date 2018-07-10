using Assets.HandShankAdapation.CodeModel;
using Assets.HandShankAdapation.Interactivity.UGUI.AdapationStrategy;
using UnityEngine;

public class ChangeGameOverStrategy : ChangeState
{

    public GameObject CanvasOver;

    public GameObject CanvasPause;

    public GameObject CanvasGame;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CanvasOver != null && CanvasOver.activeInHierarchy && OperateModeManager.Instance.CurrentStrategy != Strategy.DefaultStrategy)
            Change(Strategy.DefaultStrategy);
        else if(CanvasPause != null && CanvasPause.activeInHierarchy && OperateModeManager.Instance.CurrentStrategy != Strategy.DefaultStrategy)
            Change(Strategy.DefaultStrategy);
        else if(CanvasGame != null && CanvasGame.activeInHierarchy && OperateModeManager.Instance.CurrentStrategy != Strategy.GameStragegy)
            Change(Strategy.GameStragegy);
	}
}
