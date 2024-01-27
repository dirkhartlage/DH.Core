using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace DH.Core.Tests.TestingUtils.Editor
{
    public static class RuntimeTestUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath">E.g.: Packages/com.dh.dh_resources/Tests/RuntimeTests/TestEntities/MyPrefab.prefab</param>
        /// <returns></returns>
        public static GameObject InstantiatePrefabFromPath(string fullPath)
        {
            // Rider always complains about AssetDatabase and Prefab utility. But it will build no problem.
            // There seems to be no permanent fix at the moment. For now, go to the Problem tab at the bottom of the window -> find this file in All Solution Files -> right-click -> Ignore Errors. It stays until you flush the cache.
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
            Assert.NotNull(prefab);

            GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            Assert.NotNull(go);
            return go;
        }
    }
}