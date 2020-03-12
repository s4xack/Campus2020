using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private IImageHolder imageHolder;
        private readonly AppSettings appSettings;

        public ImageSettingsAction(IImageHolder imageHolder, AppSettings appSettings)
        {
            this.imageHolder = imageHolder;
            this.appSettings = appSettings;
        }


        public string Category => "Настройки";
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";
        public int Order => 3;

        public void Perform()
        {
            var imageSettings = appSettings.ImageSettings;
            SettingsForm.For(imageSettings).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}