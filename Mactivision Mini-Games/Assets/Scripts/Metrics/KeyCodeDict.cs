/*
FOR CONFIG FILE CONFIGURATION:
Use the keys of the dictionary entries without the word "KeyCode".
Examples:
"DigKey": "B"
"LeftKey": "LeftArrow"
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyCodeDict
{
    public static Dictionary<KeyCode, string> toString = new Dictionary<KeyCode, string>()
    {
        {KeyCode.Space, "Space"},
        {KeyCode.A, "A"},
        {KeyCode.B, "B"},
        {KeyCode.C, "C"},
        {KeyCode.D, "D"},
        {KeyCode.E, "E"},
        {KeyCode.F, "F"},
        {KeyCode.G, "G"},
        {KeyCode.H, "H"},
        {KeyCode.I, "I"},
        {KeyCode.J, "J"},
        {KeyCode.K, "K"},
        {KeyCode.L, "L"},
        {KeyCode.M, "M"},
        {KeyCode.N, "N"},
        {KeyCode.O, "O"},
        {KeyCode.P, "P"},
        {KeyCode.Q, "Q"},
        {KeyCode.R, "R"},
        {KeyCode.S, "S"},
        {KeyCode.T, "T"},
        {KeyCode.U, "U"},
        {KeyCode.V, "V"},
        {KeyCode.W, "W"},
        {KeyCode.X, "X"},
        {KeyCode.Y, "Y"},
        {KeyCode.Z, "Z"},
        {KeyCode.Alpha0, "0"},
        {KeyCode.Alpha1, "1"},
        {KeyCode.Alpha2, "2"},
        {KeyCode.Alpha3, "3"},
        {KeyCode.Alpha4, "4"},
        {KeyCode.Alpha5, "5"},
        {KeyCode.Alpha6, "6"},
        {KeyCode.Alpha7, "7"},
        {KeyCode.Alpha8, "8"},
        {KeyCode.Alpha9, "9"},
        {KeyCode.LeftArrow, "←"},
        {KeyCode.RightArrow, "→"},
        {KeyCode.UpArrow, "↑"},
        {KeyCode.DownArrow, "↓"},
    };
}
