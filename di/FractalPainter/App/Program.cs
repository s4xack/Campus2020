﻿using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();

                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>()
                    .InSingletonScope();

                container.Bind<KochPainter>().ToSelf().InSingletonScope();

                container.Bind(x => x.FromThisAssembly()
                        .SelectAllClasses()
                        .InheritedFrom<IUiAction>()
                        .BindAllInterfaces());
                /*
                container.Bind<IUiAction>().To<KochFractalAction>();
                container.Bind<IUiAction>().To<SaveImageAction>();
                container.Bind<IUiAction>().To<DragonFractalAction>();
                container.Bind<IUiAction>().To<ImageSettingsAction>();
                container.Bind<IUiAction>().To<PaletteSettingsAction>();
                */

                container.Bind<IDragonPainterFactory>().ToFactory();

                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();
                container.Bind<AppSettings>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load());

                

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
    }
}