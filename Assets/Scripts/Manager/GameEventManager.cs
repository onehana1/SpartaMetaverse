using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static event Action<bool> OnEnterDoor;
    public static event Action<bool> OnEnterCraneDoor;
    public static event Action<bool> OnEnterTomstone;


    public static void TriggerEnterDoor(bool state)
    {
        OnEnterDoor?.Invoke(state);
    }

    public static void TriggerEnterCraneDoor(bool state)
    {
        OnEnterCraneDoor?.Invoke(state);
    }
    public static void TriggerEnterTomstone(bool state)
    {
        OnEnterTomstone?.Invoke(state);
    }
}
