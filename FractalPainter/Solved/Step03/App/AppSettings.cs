using FractalPainting.Infrastructure.Common;

namespace FractalPainting.Solved.Step03.App
{
    public class AppSettings : IImageDirectoryProvider, IImageSettingsProvider
    {
        public string ImagesDirectory { get; set; }
        public ImageSettings ImageSettings { get; set; }
    }
}