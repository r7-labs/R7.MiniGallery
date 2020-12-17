namespace R7.MiniGallery.Lightboxes
{
    public static class LightboxFactory
    {
        private static readonly ILightbox _blueimpGallery = new BlueimpLightbox ();

        private static readonly ILightbox _noneBox = new Nonebox ();

        private static readonly ILightbox _lightbox2 = new Lightbox ();

        private static readonly ILightbox _colorbox = new Colorbox ();

        public static ILightbox Create (LightboxType lightboxType)
        {
            switch (lightboxType) {
                case LightboxType.BlueimpGallery:
                case LightboxType.Default:
                    return _blueimpGallery;

                case LightboxType.Lightbox:
                    return _lightbox2;

                case LightboxType.Colorbox:
                    return _colorbox;

                default:
                    return _noneBox;
            }
        }
    }
}
