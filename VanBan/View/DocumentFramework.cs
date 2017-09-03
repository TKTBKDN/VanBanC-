using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VanBan.View
{
    public class DocumentFramework : FrameworkElement
    {
        //public double Width
        //{
        //    get { return (double)GetValue(WidthProperty); }
        //    set { SetValue(WidthProperty, value); }
        //}

        //public static readonly DependencyProperty WidthProperty =
        //    DependencyProperty.Register("Width", typeof(double), typeof(DocumentFramework));

        //public double Height
        //{
        //    get { return (double)GetValue(HeightProperty); }
        //    set { SetValue(HeightProperty, value); }
        //}
        //public static readonly DependencyProperty HeightProperty =
        //    DependencyProperty.Register("Height", typeof(double), typeof(DocumentFramework));
        #region TextProperty
        /// <summary>
        /// Lấy hoặc cài đặt thuộc tính nội dung cho ...
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); InvalidateVisual(); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DocumentFramework), new PropertyMetadata(string.Empty, TextChangedCallBack));

        private static void TextChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DocumentFramework).InvalidateVisual();
        }

        #endregion
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            try
            {
                string st = "";
                double sumWidth = 0;
                st = Text;
                int viTriDau = 0;
                int viTriCuoiKT = -1;
                string chuoiTam = "";
                int vt = 0;
                int doDaiXau = Text.Length;
                for (int i = 0; i < doDaiXau; i++)
                {
                    var charSt = new FormattedText(st[i].ToString(), CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                                             new Typeface("Arial"), 14, Brushes.Black);
                    sumWidth += charSt.WidthIncludingTrailingWhitespace;
                    if (sumWidth > ActualWidth)
                    {
                        sumWidth = 0;
                        chuoiTam = st.Substring(viTriDau, i - viTriDau + 1);
                        viTriCuoiKT = chuoiTam.LastIndexOf("_");
                        if (viTriCuoiKT == -1)
                        {
                            st = st.Substring(0, i) + "\n" + st.Substring(i);
                            viTriDau = i + 1;
                            i = i;
                            sumWidth = 0;
                            doDaiXau++;
                        }
                        else if (st[i] != '_')
                        {
                            st = st.Substring(0, viTriCuoiKT + 1) + "\n" + st.Substring(viTriCuoiKT + 1);
                            viTriDau = viTriCuoiKT + 2;
                            i = viTriCuoiKT + 1;
                            sumWidth = 0;
                            doDaiXau++;
                        }
                        else
                        {
                            vt = viTriDauTienKhacKT(st, i);
                            if (vt != 0)
                            {
                                st = st.Substring(0, vt) + "\n" + st.Substring(vt);
                                viTriDau = vt + 1;
                                i = vt;
                                sumWidth = 0;
                                doDaiXau++;
                            }
                            else break;
                        }
                    }
                }

                FormattedText _formattedText = new FormattedText(
                    st, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                    new Typeface("Arial"), 14, Brushes.Red);
                drawingContext.DrawText(_formattedText, new Point(0, 0));
                if (_formattedText.WidthIncludingTrailingWhitespace > this.ActualWidth)
                {

                }

               
            }
            catch (Exception e)
            {

            }
        }

        public int viTriDauTienKhacKT(string s, int kt)
        {
            for (int j = kt; j < s.Length; j++)
            {
                if (s[j] != '_') return j;
            }
            return 0;
        }
    }
}
