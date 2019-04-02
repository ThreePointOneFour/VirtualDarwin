using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenSink {

    public class Looper
    {

        private float timer = 0.0f;
        private float LoopAt;
        private System.Action loopFunc;

        public Looper(float LoopAt, bool instant = true, System.Action loopFunc = null)
        {
            this.LoopAt = LoopAt;
            if (instant) timer = LoopAt;
            if (loopFunc != null) this.loopFunc = loopFunc;
        }

        public void Loop(float deltaTime)
        {
            if (loopFunc == null) return;

            timer += deltaTime;
            if (timer >= LoopAt)
            {
                loopFunc();
                timer = 0.0f;
            }
        }

        public void Add(System.Action addFunc) {
            loopFunc += addFunc;
        }

        public void Remove(System.Action removeFunc) {
            loopFunc -= removeFunc;
        }
    }

    public static class BaseU
    {
        public static int Char2int(char c)
        {
            return (int)char.GetNumericValue(c);
        }

        public static int Str2int(string s)
        {
            return int.Parse(s);
        }

        public static string ChangeStrAt(string str, char change, int at)
        {
            string front = str.Substring(0, at);
            string back = str.Substring(at + 1, str.Length - (at + 1));
            return front + change + back;
        }

        public static Point GetMatrixMid<T>(T[,] matrix)
        {
            return new Point((matrix.GetLength(0) / 2), (matrix.GetLength(1) / 2));
        }

        public static int GetEnumLength(System.Type enumType) {
            return System.Enum.GetValues(enumType).Length;
        }

        public static string[] StringSplit(string str, int chunks = 2) {
            string[] ret = new string[chunks];

            int strLen = str.Length;
            float chunkLength = str.Length / (float)chunks;
            float counter = 0f;
            for (int i = 0; i < chunks; i++) {

                int start = Mathf.FloorToInt(counter);
                counter += chunkLength;
                int end = Mathf.FloorToInt(counter);

                ret[i] = str.Substring(start, end - start);
            }
            return ret;
        }

        public static float StringPercentageCompare(string s1, string s2) {
            int matching = 0;

            int minLength = Mathf.Min(s1.Length, s2.Length);
            int maxLength = Mathf.Max(s1.Length, s2.Length);

            for (int i = 0; i < minLength; i++) {
                if (s1[i] == s2[i])
                    matching++;
            }
            return matching / maxLength;
        }

    }

    public static class MathU {
        public static float Onefy(float nmb) {
            if (nmb > 0)
                return 1;
            else if (nmb < 0)
                return -1;
            else
                return 0;
        }

        public static int MinMax(int nmb, int min, int max)
        {
            return Mathf.Min(Mathf.Max(nmb, min), max);
        }

        public static int PowOfTen(int nmb)
        {
            return Mathf.RoundToInt(Mathf.Pow(10.0F, (float)nmb));
        }

        public static float Percentify(int nmb)
        {
            return (float)nmb / (PowOfTen(NmbLenght(nmb)));
        }

        public static int NmbLenght(int nmb) {
            return ("" + nmb).Length;
        }

    }

    public static class PhysicsU
    {
        public enum Directions { None, Rigth, Up, Left, Down };
        public static Vector2 Dir2Vec(Directions dir)
        {
            switch (dir)
            {
                case Directions.None:
                    return Vector2.zero;

                case Directions.Rigth:
                    return Vector2.right;

                case Directions.Up:
                    return Vector2.up;

                case Directions.Left:
                    return Vector2.left;

                case Directions.Down:
                    return Vector2.down;

                default:
                    return Vector2.zero;

            }
        }

        public static RaycastHit2D FindNonChildRayHit(RaycastHit2D[] hits, GameObject OriginObject) {
            RaycastHit2D ret = default(RaycastHit2D);
            foreach (RaycastHit2D hit in hits) {

                if (!hit.transform.IsChildOf(OriginObject.transform)) {
                    ret = hit;
                    break;
                }
            }
            return ret;
        }

        public static void Teleport(GameObject go, Vector2 pos) {
            go.transform.position = pos;
            return;
        }
    }

    public static class RandomU {

        public static string RandomString(int length) {
            string randomString = "";
            for (int i = 0; i < length; i++) {
                randomString += (char) Random.Range(97,122);
            }
            return randomString;
        }


    }

    public static class ListU {

        public static T TryGet<T>(List<T> List, int index) {
            if (index > List.Count)
                return default;
            else return List[index];
        }

    }

    public class PrefabLoader
    {

        private static readonly IDictionary<string, IDictionary<string, GameObject>> Loaded = new Dictionary<string, IDictionary<string, GameObject>>();

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
            if (prefab == null) throw new System.Exception("Couldn't load object " + prefabName + " from: "+path+"/"+prefabName);
            return prefab;
        }

        private GameObject LoadAndAdd(string prefabName, string path, bool addPath = false)
        {
            string fullPath = (path==""?"":(path + "/")) + prefabName + "_prefab";
            GameObject prefabO = Resources.Load(fullPath) as GameObject;
            if (addPath)
            {
                IDictionary<string, GameObject> dic = new Dictionary<string, GameObject>
            {
                { prefabName, prefabO }
            };
                Loaded.Add(path, dic);
            }
            else
                Loaded[path].Add(prefabName, prefabO);
            return prefabO;
        }
    }
}
