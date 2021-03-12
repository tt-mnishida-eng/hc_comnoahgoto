namespace TT.Core.ConversionValue
{
    public static class TTCVDefine
    {
        public enum kMediationType
        {
            None,
            AdMob,
            Max,
            IronSource
        }

        public enum kValueType
        {
            DispayBanner,
            DispayRectangle,
            DispayInterstitial,
            DispayReward,
            Purchase
        }
    }

    public struct TTCVCountData
    {
        public int DispayBanner;
        public int DispayRectangle;
        public int DispayInterstitial;
        public int DispayReward;
        public int Purchase;

        public void Increment(TTCVDefine.kValueType valueType)
        {
            switch(valueType)
            {
                    case TTCVDefine.kValueType.DispayBanner:
                        DispayBanner++;
                        break;
                    case TTCVDefine.kValueType.DispayRectangle:
                        DispayRectangle++;
                        break;
                    case TTCVDefine.kValueType.DispayInterstitial:
                        DispayInterstitial++;
                        break;
                    case TTCVDefine.kValueType.DispayReward:
                        DispayReward++;
                        break;
                    case TTCVDefine.kValueType.Purchase:
                        Purchase++;
                        break;
                    default:
                        TTDebug.LogError("TTCVDefine.kValueTypeに対応したプロパティがない");
                        break;
            }
        }
    }
}