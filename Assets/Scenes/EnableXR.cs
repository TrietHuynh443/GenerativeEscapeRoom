using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class EnableXR : MonoBehaviour
{
#if UNITY_EDITOR
        private void Awake()
        {
            StartCoroutine(EnableXRCoroutine());
        }

        private void OnDestroy()
        {
            DisableXR();
        }

        private IEnumerator EnableXRCoroutine()
        {
            while (XRGeneralSettings.Instance == null)
            {
                yield return null;
            }
            
            // Make sure the XR is disabled and properly disposed. It can happen that there is an activeLoader left
            // from the previous run.
            if (XRGeneralSettings.Instance.Manager.activeLoader || XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                DisableXR();
                // Wait for the next frame, just in case
                yield return null;
            }

            // Make sure we don't have an active loader already
            if (!XRGeneralSettings.Instance.Manager.activeLoader)
            {
                yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
            }

            // Make sure we have an active loader, and the manager is initialized
            if (XRGeneralSettings.Instance.Manager.activeLoader && XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                XRGeneralSettings.Instance.Manager.StartSubsystems();
            }
        }

        /// <summary>
        /// Disables XR
        /// </summary>
        private void DisableXR()
        {
            // Make sure there is something to de-initialize
            if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                XRGeneralSettings.Instance.Manager.StopSubsystems();
                XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            }
        }
#endif
}