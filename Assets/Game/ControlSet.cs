using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eControl { Up, Down, Left, Right, A, B};
public class ControlSet
{
    public Dictionary<eControl, KeyCode> _controls;

    public ControlSet(Dictionary<eControl, KeyCode> controls)
    {
        _controls = controls;
    }

    public static ControlSet P1()
    {
        Dictionary<eControl, KeyCode> controls = new Dictionary<eControl, KeyCode>
        {
            {eControl.Up, KeyCode.W },
            {eControl.Down, KeyCode.S },
            {eControl.Left, KeyCode.A },
            {eControl.Right, KeyCode.D },
            {eControl.A, KeyCode.E},
            {eControl.B, KeyCode.Q }
        };
        return new ControlSet(controls);
    }
    public static ControlSet P2()
    {
        Dictionary<eControl, KeyCode> controls = new Dictionary<eControl, KeyCode>
        {
            {eControl.Up, KeyCode.UpArrow },
            {eControl.Down, KeyCode.DownArrow },
            {eControl.Left, KeyCode.LeftArrow },
            {eControl.Right, KeyCode.RightArrow },
            {eControl.A, KeyCode.PageUp},
            {eControl.B, KeyCode.PageDown }
        };
        return new ControlSet(controls);
    }
}
