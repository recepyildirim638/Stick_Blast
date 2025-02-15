using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdsManager 
{
    void LoadAds();
    void ShowBanner();
    void HideBanner();
    void ShowInterstitial();
    void ShowRewardedAds();
}

public class IronSouceAdsManager : IAdsManager
{
    public void LoadAds()
    {

    }
    public void HideBanner()
    {
        
    }

    public void ShowBanner()
    {
       
    }

    public void ShowInterstitial()
    {
        
    }

    public void ShowRewardedAds()
    {
     
    }
}

public class AdmobAdsManager : IAdsManager
{
    public void HideBanner()
    {
        throw new System.NotImplementedException();
    }

    public void LoadAds()
    {
        throw new System.NotImplementedException();
    }

    public void ShowBanner()
    {
        throw new System.NotImplementedException();
    }

    public void ShowInterstitial()
    {
        throw new System.NotImplementedException();
    }

    public void ShowRewardedAds()
    {
        throw new System.NotImplementedException();
    }
}