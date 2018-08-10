using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAUtils : MonoBehaviour {

    public static int Char2int(char c) {
        return (int)char.GetNumericValue(c);
    }

    public static int Str2int(string s)
    {
        return Int32.Parse(s);
    }
}
