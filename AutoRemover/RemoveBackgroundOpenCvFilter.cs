﻿using OpenCvSharp;

namespace Keyer.AutoRemover
{
    /// <inheritdoc />
    /// <summary>
    ///     Makes background transparent on a given image using edge detection.
    /// </summary>
    public class RemoveBackgroundOpenCvFilter : OpenCvFilter
    {
        private const double FloodFillRelativeSeedPoint = 0.01;
        private double _floodFillTolerance = 0.01;

        /// <summary>
        ///     Tolerance of flood fill applied to mask.
        /// </summary>
        public double FloodFillTolerance
        {
            get => _floodFillTolerance;
            set
            {
                if (value > 1)
                    throw new ArgumentException("Flood fill tolerance should be less then 1.");
                if (value < 0)
                    throw new ArgumentException("Flood fill tolerance should be greater than or equal to zero.");

                _floodFillTolerance = value;
            }
        }

        /// <summary>
        ///     Amount of blur applied to the transparency mask on a final stage.
        /// </summary>
        public int MaskBlurFactor { get; set; } = 5;

        public override IEnumerable<MatType> SupportedMatTypes => new[] { MatType.CV_8UC3, MatType.CV_8UC4 };

        protected override void ProcessFilter(Mat src, Mat dst)
        {
            using (Mat alphaMask = GetGradient(src))
            {
                // Performs morphology operation on alpha mask with resolution-dependent element size
                void PerformMorphologyEx(MorphTypes operation, int iterations)
                {
                    double elementSize = Math.Sqrt(alphaMask.Width * alphaMask.Height) / 300;
                    if (elementSize < 3)
                        elementSize = 3;

                    if (elementSize > 20)
                        elementSize = 20;

                    using (var se = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(elementSize, elementSize)))
                    {
                        Cv2.MorphologyEx(alphaMask, alphaMask, operation, se, null, iterations);
                    }
                }

                PerformMorphologyEx(MorphTypes.Dilate, 1); // Close small gaps in edges

                Cv2.FloodFill( // Flood fill outer space
                    image: alphaMask,
                    seedPoint: new OpenCvSharp.Point(
                        (int)(FloodFillRelativeSeedPoint * src.Width),
                        (int)(FloodFillRelativeSeedPoint * src.Height)),
                    newVal: new Scalar(0),
                    rect: out Rect _,
                    loDiff: new Scalar(FloodFillTolerance),
                    upDiff: new Scalar(FloodFillTolerance),
                    flags: FloodFillFlags.FixedRange | FloodFillFlags.Link4);

                PerformMorphologyEx(MorphTypes.Erode, 1); // Compensate initial dilate
                PerformMorphologyEx(MorphTypes.Open, 2); // Remove not filled small spots (noise)
                PerformMorphologyEx(MorphTypes.Erode, 1); // Final erode to remove white fringes/halo around objects

                Cv2.Threshold(
                    src: alphaMask,
                    dst: alphaMask,
                    thresh: 0,
                    maxval: 255,
                    type: ThresholdTypes.Binary); // Everything non-filled becomes white

                alphaMask.ConvertTo(alphaMask, MatType.CV_8UC1, 255);

                if (MaskBlurFactor > 0)
                    Cv2.GaussianBlur(alphaMask, alphaMask, new OpenCvSharp.Size(MaskBlurFactor, MaskBlurFactor), MaskBlurFactor);

                AddAlphaChannel(src, dst, alphaMask);
            }
        }

        /// <summary>
        ///     Adds transparency channel to source image and writes to output image.
        /// </summary>
        private static void AddAlphaChannel(Mat src, Mat dst, Mat alpha)
        {
            var bgr = Cv2.Split(src);
            var bgra = new[] { bgr[0], bgr[1], bgr[2], alpha };
            Cv2.Merge(bgra, dst);
        }

        /// <summary>
        ///     Performs edges detection. Result will be used as base for transparency mask.
        /// </summary>
        private Mat GetGradient(Mat src)
        {
            using (var preparedSrc = new Mat())
            {
                Cv2.CvtColor(src, preparedSrc, ColorConversionCodes.BGR2GRAY);
                preparedSrc.ConvertTo(preparedSrc, MatType.CV_32FC1, 1.0 / 255);

                // Calculate Sobel derivative with kernel size depending on image resolution
                Mat Derivative(int dx, int dy)
                {
                    int resolution = preparedSrc.Width * preparedSrc.Height;
                    int kernelSize =
                        resolution < 1280 * 1280 ? 3 : // Larger image --> larger kernel
                        resolution < 2000 * 2000 ? 5 :
                        resolution < 3000 * 3000 ? 9 :
                                                   15;
                    float kernelFactor = kernelSize == 3 ? 1 : 2; // Compensate lack of contrast on large images

                    using (var kernelRows = new Mat())
                    using (var kernelColumns = new Mat())
                    {
                        // Get normalized Sobel kernel of desired size
                        Cv2.GetDerivKernels(kernelRows, kernelColumns, dx, dy, kernelSize, normalize: true);

                        using (var multipliedKernelRows = kernelRows * kernelFactor)
                        using (var multipliedKernelColumns = kernelColumns * kernelFactor)
                        {
                            return preparedSrc.SepFilter2D(MatType.CV_32FC1, multipliedKernelRows, multipliedKernelColumns);
                        }
                    }
                }

                using (var gradX = Derivative(1, 0))
                using (var gradY = Derivative(0, 1))
                {
                    var result = new Mat();
                    Cv2.Magnitude(gradX, gradY, result);

                    result += 0.15f; //Add small constant so the flood fill will perform correctly
                    return result;
                }
            }
        }
    }
}