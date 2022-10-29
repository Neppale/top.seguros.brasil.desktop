namespace Top_Seguros_Brasil_Desktop.src.font
{
    internal class TsbFont
    {

        public static PrivateFontCollection TsbFonts = new PrivateFontCollection();



        public TsbFont()
        {
            try
            {
                TsbFonts.AddFontFile("src\\font\\Roboto-Black.ttf");
                TsbFonts.AddFontFile("src\\font\\Roboto-Bold.ttf");
                TsbFonts.AddFontFile("src\\font\\Roboto-Light.ttf");
                TsbFonts.AddFontFile("src\\font\\Roboto-Medium.ttf");
                TsbFonts.AddFontFile("src\\font\\Roboto-Regular.ttf");
                TsbFonts.AddFontFile("src\\font\\Roboto-Thin.ttf");
            }
            catch
            {
                TsbFonts.AddFontFile("..\\..\\..\\src\\font\\Roboto-Black.ttf");
                TsbFonts.AddFontFile("..\\..\\..\\src\\font\\Roboto-Bold.ttf");
                TsbFonts.AddFontFile("..\\..\\..\\src\\font\\Roboto-Light.ttf");
                TsbFonts.AddFontFile("..\\..\\..\\src\\font\\Roboto-Medium.ttf");
                TsbFonts.AddFontFile("..\\..\\..\\src\\font\\Roboto-Regular.ttf");
                TsbFonts.AddFontFile("..\\..\\..\\src\\font\\Roboto-Thin.ttf");
            }

        }
    }
}
