﻿using JetBrains.Annotations;
using OpenCvSharp;
using System.Diagnostics;

namespace BackgroundRemovalSample.App
{
    internal class Remover
    {
        public Remover(string inputPath, string outputPath)
        {
            if (inputPath == null || outputPath == null)
                throw new ArgumentException("Input Path, or output Path does not exist.");

            _args = new RemoverArguments() { InputImagePath = inputPath, OutputImagePath = outputPath };
        }

        private readonly RemoverArguments _args;
        public async Task Run()
        {
            using (var img = new Mat(_args.InputImagePath))
            {
                var filter = new RemoveBackgroundOpenCvFilter
                {
                    FloodFillTolerance = _args.FloodFillTolerance,
                    MaskBlurFactor = _args.MaskBlurFactor
                };

                var sw = new Stopwatch();
                sw.Start();
                using (var result = filter.Apply(img))
                {
                    sw.Stop();
                    Console.WriteLine($"Run {sw.ElapsedMilliseconds}ms");

                    result.SaveImage(_args.OutputImagePath);
                }
            }
        }

        [UsedImplicitly]
        public class RemoverArguments
        {
            public String InputImagePath { get; set; }
            public String OutputImagePath { get; set; }
            public Double FloodFillTolerance { get; set; } = 0.01;
            public Int32 MaskBlurFactor { get; set; } = 5;
        }
    }
}
