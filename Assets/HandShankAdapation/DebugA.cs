using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GDGeek;

public class DebugA : Singleton<DebugA>
{
    protected static Dictionary<string, int> debug = new Dictionary<string, int>();
    protected static StringBuilder debugstr = new StringBuilder();
    // Use this for initialization
    public static void Log(object o)
    {
        string d = o.ToString();
        if(debug.ContainsKey(d)) debug[d]++;
        else debug.Add(d,1);
        debugstr = new StringBuilder();
        foreach (var pair in debug)
        {
            debugstr.Append(Environment.NewLine);
            debugstr.Append(pair.Key);
            debugstr.Append("  :  ");
            debugstr.Append(pair.Value.ToString());
        }
    }

    void OnGUI()
    {
        var fontstyle = new GUIStyle();
        fontstyle.normal.textColor = Color.black;
        fontstyle.fontSize = 20;
        
        GUI.Label(new Rect(0, 0, 200, 200), debugstr.ToString(), fontstyle);
    }
}
