
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;



namespace Model
{
    public abstract class ModelAbstractApi 
    {
        public abstract int width { get; }
        public abstract int height { get; }
        public abstract Canvas Canvas { get; set; }
        public abstract List<Ellipse> ellipseCollection { get; }
        public abstract void CreateEllipses(int ballVal);
        public abstract void Move();


        public static ModelAbstractApi CreateApi(int Weight, int Height)
        {
            return new ModelApi(Weight, Height);
        }
    }
    internal class ModelApi : ModelAbstractApi
    {
        public override int width { get; }
        public override int height { get; }

        private LogicAbstractApi LogicLayer;
        public override List<Ellipse> ellipseCollection { get;}
        public override Canvas Canvas { get; set; }
        public ModelApi(int Width, int Height)
        {
  
            this.width = Width;
            this.height = Height;
            LogicLayer = LogicAbstractApi.CreateApi(width, height);
            ellipseCollection = new List<Ellipse>();
            Canvas = new Canvas();
            Canvas.HorizontalAlignment = HorizontalAlignment.Center;
            Canvas.VerticalAlignment = VerticalAlignment.Top;
            Canvas.Width = width;
            Canvas.Height = height;
            Canvas.Background = new SolidColorBrush(Color.FromRgb(241, 237, 237));
            LogicLayer.Update += (sender, args) => Move();
        }
        public override void CreateEllipses(int ballVal)
        {
            LogicLayer.CreateBallsList(ballVal);

            for (int i = LogicLayer.balls.Count - ballVal; i < LogicLayer.balls.Count; i++)
            {
                Ellipse ellipse = new Ellipse { Width = LogicLayer.balls[i].Size, Height = LogicLayer.balls[i].Size, Fill = Brushes.Black };
                Canvas.SetLeft(ellipse, LogicLayer.balls[i].X);
                Canvas.SetTop(ellipse, LogicLayer.balls[i].Y);
                ellipseCollection.Add(ellipse);
                Canvas.Children.Add(ellipse);
            }
            LogicLayer.Start();
           
        }

        public override void Move()
        {
            for (int i = 0; i < LogicLayer.balls.Count; i++)
            {
               
                Canvas.SetLeft(ellipseCollection[i], LogicLayer.balls[i].X);
                Canvas.SetTop(ellipseCollection[i], LogicLayer.balls[i].Y);
            }

        }




    }

}
