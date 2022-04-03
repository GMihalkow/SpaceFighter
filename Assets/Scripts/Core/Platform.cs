namespace SpaceFighter.Core
{
    public static class Platform
    {
        public static bool IsMobile
        {
            get
            {
#if UNITY_EDITOR
                return false;
#elif UNITY_WEBGL
    return WebGLHandler.IsMobileBrowser();
#else
    return false;
#endif
            }
        }
    }
}