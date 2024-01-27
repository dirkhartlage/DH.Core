using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace DH.Core.Test
{
    public class TestBase
    {
        /// <summary>
        /// We need to wait a frame to let the Destroy operations go through and the OnDestroy event functions to do their thing
        /// </summary>
        /// <returns></returns>
        [UnitySetUp]
        public IEnumerator TearDown()
        {
            yield return null;
        }
    }
}