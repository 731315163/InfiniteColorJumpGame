using ColorSwitchGame.Types;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{

    protected Image image;

    protected Text text;
    public string CurrentString;
    protected string nextstr;

    public Unlockable State;
	// Use this for initialization
	void Start ()
	{
	    image = this.GetComponent<Image>();
        image.color = new Color(1,1,1,0);
	    text = this.GetComponentInChildren<Text>();

	}

    public void ChangeTextShow(string s)
    {
        text.text = s;
        nextstr = s;
    }

    public void NotSelect()
    {
        text.text = nextstr;
        image.color = new Color(1,1,1,0);
    }

    public void Select()
    { 
        nextstr = text.text;
        text.text = CurrentString;
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        image.color = Color.white;
    }
}
