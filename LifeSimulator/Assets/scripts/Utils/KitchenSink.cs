using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenSink {
    public class Looper
    {

        private float timer = 0.0f;
        private float LoopAt;
        private Action loopFunc;

        public Looper(Action loopFunc, float LoopAt, bool instant=true)
        {
            this.LoopAt = LoopAt;
            this.loopFunc = loopFunc;
            if (instant) timer = LoopAt;
        }

        public void Loop(float deltaTime)
        {
            timer += deltaTime;
            if (timer <= LoopAt)
            {
                loopFunc();
                timer = 0.0f;
            }
        }
    }

    public static class Utils
    {

        public static int Char2int(char c)
        {
            return (int)char.GetNumericValue(c);
        }

        public static int Str2int(string s)
        {
            return Int32.Parse(s);
        }

        public static string ChangeStrAt(string str, char change, int at)
        {
            string front = str.Substring(0, at);
            string back = str.Substring(at + 1, str.Length - (at + 1));
            return front + change + back;
        }

        public static Point GetMatrixMid(int[,] matrix)
        {
            return new Point((matrix.GetLength(0) / 2), (matrix.GetLength(1) / 2));
        }
    }

    public class PrefabLoader
    {

        private static readonly IDictionary<string, IDictionary<string, GameObject>> Loaded;

        public GameObject Load(string prefabName, string path = "")
        {
            GameObject prefab;
            IDictionary<string, GameObject> temp;
            if (!Loaded.TryGetValue(path, out temp))
                prefab = LoadAndAdd(prefabName, path, true);
            else
            {
                if (!temp.TryGetValue(prefabName, out prefab))
                    prefab = LoadAndAdd(prefabName, path);
            }
            return prefab;
        }

        private GameObject LoadAndAdd(string prefabName, string path, bool addPath = false)
        {
            GameObject prefabO = Resources.Load(path + "/" + prefabName + "_prefab") as GameObject;
            if (addPath)
            {
                IDictionary<string, GameObject> dic = new Dictionary<string, GameObject>
            {
                { prefabName, prefabO }
            };
                Loaded.Add("path", dic);
            }
            else
                Loaded[path].Add(prefabName, prefabO);
            return prefabO;
        }
    }
}
