using UnityEngine;

namespace TMG.Survivors
{
    public class CameraTargetSingleton : MonoBehaviour
    {
        public static CameraTargetSingleton Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("CameraTargetSingleton already exists. Destroying new instance.");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}