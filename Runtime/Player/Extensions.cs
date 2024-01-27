using UnityEngine;

namespace DH.Core.Player
{
    public static class Extensions
    {
        public static bool IsLocalPlayer(this Component comp)
            => comp.GetComponent<LocalPlayerHandle>();
    }
}