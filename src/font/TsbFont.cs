using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.font
{
    internal class TsbFont 
    {

        public static PrivateFontCollection TsbFonts = new PrivateFontCollection();

        
        
        public TsbFont()
        {
            TsbFonts.AddFontFile("src\\font\\Roboto-Black.ttf");
            TsbFonts.AddFontFile("src\\font\\Roboto-Bold.ttf");
            TsbFonts.AddFontFile("src\\font\\Roboto-Light.ttf");
            TsbFonts.AddFontFile("src\\font\\Roboto-Medium.ttf");
            TsbFonts.AddFontFile("src\\font\\Roboto-Regular.ttf");
            TsbFonts.AddFontFile("src\\font\\Roboto-Thin.ttf");

        }
    }
}
