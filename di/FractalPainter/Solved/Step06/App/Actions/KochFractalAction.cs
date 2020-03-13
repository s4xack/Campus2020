﻿using FractalPainting.Solved.Step06.App.Fractals;
using FractalPainting.Solved.Step06.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step06.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private readonly KochPainter kochPainter;

        public KochFractalAction(KochPainter kochPainter)
        {
            this.kochPainter = kochPainter;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            kochPainter.Paint();
        }
    }
}