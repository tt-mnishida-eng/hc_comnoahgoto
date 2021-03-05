using UnityEngine;

namespace Common.Extension
{
    public static class MonobehaviourExtensions
    {
        public static void SetActive(this MonoBehaviour self, bool isActive)
        {
            if (self.gameObject.activeSelf != isActive)
                self.gameObject.SetActive(isActive);
        }
    }
}