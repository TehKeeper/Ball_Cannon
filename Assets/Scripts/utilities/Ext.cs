using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using utilities.tools;
using Object = UnityEngine.Object;

namespace utilities
{
    public static class Ext
    {
        public static string IsNull(this object obj)
        {
            return $"{obj?.ToString().Col(ClrUtil.Object) ?? "null".Col(ClrUtil.RedOrange)}";
        }

        public static Transform FindTransform(this Transform tf, string name) => Util.FindTransform(name, tf);

        public static T FindComponent<T>(this Transform tf, string name) where T : Component =>
            Util.FindComponent<T>(name, tf);

        public static T[] FindComponents<T>(this Transform tf, params string[] names) where T : Component
        {
            if (names.Length == 0)
            {
                Debug.LogError("Can't find items because item list is empty");
                return new T[0];
            }
            
            var l1 = new List<T>();
            foreach (var nm in names)
            {
                l1.Add(Util.FindComponent<T>(nm, tf));
            }

            return l1.ToArray();
        }

        public static void Enable(this CanvasGroup cg, bool b, bool objToo = false) => Util.CgControl(cg, b, objToo);

        public static void Clear(this Transform tf)
        {
            foreach (Transform child in tf)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static string Col(this object obj, Color clr) => TextUtilities.ColText(obj.ToString(), clr);

        public static string Col(this object obj, System.Drawing.Color clr) =>
            TextUtilities.ColText(obj.ToString(), new Color32(clr.R, clr.G, clr.B, clr.A));

        public static string Print<T>(this IEnumerable<T> array, string divide = "\n") =>
            Util.PrintArray(array, divide);

        public static string Print<T1, T2>(this Dictionary<T1, List<T2>> array, string divide = "\n") =>
            array.Select(v => $"~{v.Key}~ {Util.PrintArray(v.Value, ", ")}{divide}").Print();


        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static T Random<T>(this IEnumerable<T> arr)
        {
            var enumerable = arr as T[] ?? arr.ToArray();
            if (enumerable.Length == 1)
                return enumerable[0];
            return enumerable.ToArray()[UnityEngine.Random.Range(0, enumerable.Count())];
        }


        public static List<T> Unwrap<T>(this List<List<T>> arg0)
        {
            var outp = new List<T>();
            arg0.ForEach(v => outp.AddRange(v));
            return outp;
        }

        public static Dictionary<T1, T2> KvpToDict<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> arg0) =>
            arg0.ToDictionary(a1 => a1.Key, a2 => a2.Value);

        public static string Col<T1, T2>(this bool b) => TextUtilities.ColBool(b);

        public static T[] Flatten<T>(this T[,,] input)
        {
            int x = input.GetLength(0);
            int y = input.GetLength(1);
            int z = input.GetLength(2);
            var outp = new T[x * y * z];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < z; k++)
                    {
                        outp[Util.Get3dCoord(x, y, z, i, j, k)] = input[i, j, k];
                        //Debug.Log($"Input value: {input[i, j, k]}, flatten value: {outp[i + y * z + j * z + k]}");
                    }
                }
            }

            Debug.Log($"Outp values:\n{outp.Print()}");
            return outp;
        }

        public static T[,,] To3d<T>(this T[] input, int x, int y, int z)
        {
            var outp = new T[x, y, z];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < z; k++)
                    {
                        outp[i, j, k] = input[i * y * z + j * z + k];
                    }
                }
            }

            return outp;
        }

        public static void TryAddList<T1, T2>(this Dictionary<T1, List<T2>> dict, T1 key, T2 value)
        {
            if (dict.ContainsKey(key))
                dict[key].Add(value);
            else
                dict.Add(key, new List<T2>(new[] {value}));
        }

        public static string ColBool(this bool v) => $"<color=#{(!v ? "ee0000" : "0000ee")}>{v}</color>";

        public static bool Approx(this float v1, float v2) => Mathf.Approximately(v1, v2);
        public static float RoundUp(this float v1, int v2) => (float) Math.Round(v1, v2);

        public static string StrJoin(this IEnumerable<string> range, string sep) => string.Join(sep, range);

        public static TV GetOrNull<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, string log = "")
        {
            dictionary.TryGetValue(key, out var value);
            if (value == null && !log.IsNullOrEmpty())
                Debug.Log(log);
            return value;
        }

        public static bool TryAdd<T>(this List<T> list, T obj)
        {
            if (list.Contains(obj))
                return false;
            list.Add(obj);
            return true;
        }

        public static bool TryRemove<T>(this List<T> list, T obj)
        {
            if (!list.Contains(obj))
                return false;
            list.Remove(obj);
            return true;
        }

        public static void Sub(this Action targ, Action action, bool sub = true)
        {
            if (sub)
                targ += action;
            else
                targ -= action;
        }

        public static void Sub<T>(this Action<T> targ, Action<T> action, bool sub = true)
        {
            if (sub)
                targ += action;
            else
                targ -= action;
        }

        public static void Sub<T1, T2>(this Action<T1, T2> targ, Action<T1, T2> action, bool sub = true)
        {
            if (sub)
                targ += action;
            else
                targ -= action;
        }

        public static void Sub<T1, T2, T3>(this Action<T1, T2, T3> targ, Action<T1, T2, T3> action, bool sub = true)
        {
            if (sub)
                targ += action;
            else
                targ -= action;
        }

        public static WrapArray<Pair<T1, T2>> DictToWrap<T1, T2>(this Dictionary<T1, T2> targ) =>
            new WrapArray<Pair<T1, T2>>(targ.Select(v => new Pair<T1, T2>(v.Key, v.Value)));
        
        public static Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> collection) =>
            collection.ToDictionary(v1 => v1.Key, v2 => v2.Value);
        
        public static Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<Pair<T1, T2>> collection) =>
            collection.ToDictionary(v1 => v1.a, v2 => v2.b);

        public static void Toggle(this bool b) => b = !b;
    }
}