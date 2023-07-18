using UnityEngine;

namespace Scripts.Data
{
    public static class DataExtensions
    {
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
    }
}