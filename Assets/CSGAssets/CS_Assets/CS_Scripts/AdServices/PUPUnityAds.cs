using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

namespace Puppeteer
{
    /// <summary>
    /// This script handles all UnityAds functions, calling an ad after a certain number of level loads, or showing a rewarded ad
    /// </summary>
    public class PUPUnityAds : MonoBehaviour
    {
        [Tooltip("How many times should a level be loaded before showing an ad")]
        public int showAdEvery = 3;
        internal int showAdCount = 0;

        public bool countOnLoad = true;

        public void Awake()
        {
            #if UNITY_ADS
			showAdCount = PlayerPrefs.GetInt("UnityAdsCount", showAdCount);
			#endif
        }
		
#if UNITY_ADS
        public void CountAd()
        {
            if ( showAdCount < showAdEvery )
            {
                showAdCount++;

                PlayerPrefs.SetInt("UnityAdsCount", showAdCount);
            }
            else
            {
                showAdCount = 0;

                ShowAd();
            }
        }

        public void ShowAd()
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }

        public void OnLevelWasLoaded()
        {
            if ( countOnLoad == true )    CountAd();
        }
#endif

    }
}
