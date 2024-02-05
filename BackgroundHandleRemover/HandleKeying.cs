namespace BackgroundHandleRemover
{
    public class HandleKeying
    {
        public Bitmap MakeTransparent(Bitmap bitmap, Color color, int tolerance)
        {
            Bitmap transparentImage = new Bitmap(bitmap);

            for (int i = transparentImage.Size.Width - 1; i >= 0; i--)
            {
                for (int j = transparentImage.Size.Height - 1; j >= 0; j--)
                {
                    var currentColor = transparentImage.GetPixel(i, j);
                    if (Math.Abs(color.R - currentColor.R) < tolerance &&
                         Math.Abs(color.G - currentColor.G) < tolerance &&
                         Math.Abs(color.B - currentColor.B) < tolerance)
                        transparentImage.SetPixel(i, j, color);
                }
            }

            transparentImage.MakeTransparent(color);
            return transparentImage;
        }
    }
}
