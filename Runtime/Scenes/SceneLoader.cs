using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DH.Core.Scenes
{
    public static class SceneLoader
    {
        public static IEnumerator LoadSceneAsyncCoroutine(string sceneName, LoadSceneMode mode, bool preload = true, IEnumerable<string> unloadSceneNames = null)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, mode);

            if (preload)
            {
                asyncLoad.allowSceneActivation = false;
                while (asyncLoad.progress < 0.9f)
                    yield return null;
                asyncLoad.allowSceneActivation = true;

                while (!asyncLoad.isDone)
                    yield return null;
            }

            if (unloadSceneNames != null)
            {
                foreach (string name in unloadSceneNames)
                    SceneManager.UnloadSceneAsync(name);
            }
        }

        public static IEnumerator LoadScenesAsyncCoroutine(Collection<string> sceneNames, bool preload = true, IEnumerable<string> unloadSceneNames = null)
        {
            AsyncOperation[] asyncLoads = new AsyncOperation[sceneNames.Count];

            if (preload)
            {
                // start loading
                {
                    int i = 0;
                    foreach (string sceneName in sceneNames)
                    {
                        asyncLoads[i] = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                        asyncLoads[i].allowSceneActivation = false;
                        i++;
                    }
                }

                // preload to 90%. (The last 10% are reserved - see unity docs on async loading)
                bool allPreloaded;
                do
                {
                    allPreloaded = true;

                    for (int i = 0; i < asyncLoads.Length; i++)
                    {
                        // preload to 90%. (The last 10% are reserved - see unity docs on async loading)
                        if (asyncLoads[i].progress < 0.9f)
                        {
                            allPreloaded = false;
                            break;
                        }
                    }

                    yield return null;
                } while (!allPreloaded);

                // allow switching to new scene
                foreach (var operation in asyncLoads)
                    operation.allowSceneActivation = true;

                // wait until finished
                bool allLoaded;
                do
                {
                    allLoaded = true;

                    for (int i = 0; i < asyncLoads.Length; i++)
                    {
                        if (!asyncLoads[i].isDone)
                        {
                            allLoaded = false;
                            break;
                        }
                    }

                    yield return null;
                } while (!allLoaded);
            }
            else // without preload
            {
                int i = 0;
                foreach (string sceneName in sceneNames)
                {
                    asyncLoads[i] = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                    i++;
                }
            }

            // unload
            if (unloadSceneNames != null)
            {
                foreach (string name in unloadSceneNames)
                    SceneManager.UnloadSceneAsync(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onPreloadedCallback">The callback that will be invoked once everything has been fully preloaded. Just put allowSceneActivation = true to finish the load.</param>
        /// <param name="sceneNames"></param>
        /// <returns></returns>
        public static IEnumerator PreloadScenesAsyncCoroutine(Action<AsyncOperation[]> onPreloadedCallback, Collection<string> sceneNames)
        {
            AsyncOperation[] asyncLoads = new AsyncOperation[sceneNames.Count];
            int i = 0;
            foreach (string sceneName in sceneNames)
            {
                asyncLoads[i] = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                asyncLoads[i].allowSceneActivation = false;
                i++;
            }

            bool allPreloaded;
            do
            {
                allPreloaded = true;

                for (i = 0; i < asyncLoads.Length; i++)
                {
                    // preload to 90%. (The last 10% are reserved - see unity docs on async loading)
                    if (asyncLoads[i].progress < 0.9f)
                    {
                        allPreloaded = false;
                        break;
                    }
                }

                yield return null;
            } while (!allPreloaded);

            onPreloadedCallback?.Invoke(asyncLoads);
        }

        public static void UnloadScenesAsync(params string[] sceneNames)
        {
            AsyncOperation[] asyncOps = new AsyncOperation[sceneNames.Length];

            int i = 0;
            foreach (string sceneName in sceneNames)
                asyncOps[i++] = SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}