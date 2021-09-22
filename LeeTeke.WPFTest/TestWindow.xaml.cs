using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeeTeke.WPFTest
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
            this.Loaded += TestWindow_Loaded;
        }

        private void TestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RoundSet(0.9);
        }

        private void RoundSet(double percentValue)
        {

            double angel= percentValue*360; //角度

            double radius = 58;//半径

            double leftStart = 58+4;
            double topStart = 4;

            double endLeft = 0;
            double endTop = 0;

            bool isLagreCircel = false;  //是否优势弧？？？
            double ra = 0;//弧度
            switch (angel)
            {
                case <= 90:
                    ra = (90 - angel) * Math.PI / 180;
                    endLeft = leftStart + Math.Cos(ra) * radius;
                    endTop = topStart + radius - Math.Sin(ra) * radius;
                    break;
                case   <= 180:
                    ra = (angel-90) * Math.PI / 180;
                    endLeft = leftStart + Math.Cos(ra) * radius;
                    endTop = topStart + radius + Math.Sin(ra) * radius;
                    break;

                case <= 270:
                    isLagreCircel = true;
                    ra = (angel - 180) * Math.PI / 180;
                    endLeft = leftStart - Math.Cos(ra) * radius;
                    endTop = topStart + radius + Math.Sin(ra) * radius;
                    break;

                case < 360:
                    isLagreCircel = true;
                    ra = (angel - 280) * Math.PI / 180;
                    endLeft = leftStart - Math.Cos(ra) * radius;
                    endTop = topStart + radius - Math.Sin(ra) * radius;
                    break;

                default:
                    isLagreCircel = true;
                    endLeft = leftStart - 0.001;
                    endTop = topStart;
                    break;
            }


            Point arcEndPt=new Point(endLeft, endTop);
            Size arcSize=new Size(radius,radius);

            SweepDirection direction=SweepDirection.Clockwise;//顺时针

            ///弧形
            ArcSegment arcSegment=new ArcSegment(arcEndPt, arcSize,0,isLagreCircel,direction,true);

            //形状集合
            PathSegmentCollection pathSegments=new PathSegmentCollection();
            pathSegments.Add(arcSegment);

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint=new Point(leftStart, topStart); //起始地址
            pathFigure.Segments = pathSegments;

            PathFigureCollection pathFigures = new PathFigureCollection();
            pathFigures.Add(pathFigure);

            PathGeometry pathGeometry=new PathGeometry();
            pathGeometry.Figures = pathFigures;

         //   Data = pathGeometry;

        }

    }
}
