using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ControlsManager : MonoBehaviour
{
    [Serializable]
   private class PlayerControls
{

    public List<keyCode> keyCodes;
    public KeyCode GetKeyCode(ControlKeys controlKey)

    {
        foreach(keyCode k in keyCodes)
            if(k.controlKey == controlKey)
                return k.key;

        return  KeyCode.None;
    }


}
[Serializable]
private class keyCode
{
    public ControlKeys controlKey;
    public KeyCode key;
}

[SerializeField]
List<PlayerControls> playerControls;

public KeyCode GetKey(int PlayerID, ControlKeys controlKeys)
{
    return playerControls[PlayerID].GetKeyCode(controlKeys);
}

}
