namespace R7.MiniGallery.Lightboxes
{
    public static class LightboxFactory
    {
        private static readonly ILightbox _blueimpGallery = new BlueimpLightbox ();

        private static readonly ILightbox _noneBox = new Nonebox ();

        public static ILightbox Create (LightboxType lightboxType)
        {
            switch (lightboxType) {
                case LightboxType.BlueimpGallery:
                case LightboxType.Default:
                    return _blueimpGallery;

                default:
                    return _noneBox;
            }
        }
    }
}
