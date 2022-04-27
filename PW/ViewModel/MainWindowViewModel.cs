using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace ViewModel
{
    
    public class MainWindowViewModel : BaseViewModel
    {
        private ModelAbstractApi ModelLayer = ModelAbstractApi.CreateApi();
        private IList<Logic.Ball> b_CirclesCollection;
        private List<Logic.Ball> balls = new List<Logic.Ball>();
        private List<Ellipse> ellipseCollection = new List<Ellipse>();
        private int _BallVal;
        private double xPosition;
        private double yPosition;
         Logic.LogicAbstractApi LogicLayer = Logic.LogicAbstractApi.CreateApi(600,480);
        private Canvas canvas;
        public Canvas Canvas { get => canvas; set => canvas = value; }
        public MainWindowViewModel() : this(ModelAbstractApi.CreateApi(), Logic.LogicAbstractApi.CreateApi(600, 480))
        {
        }

        public MainWindowViewModel(ModelAbstractApi modelAbstractApi, Logic.LogicAbstractApi logicAbstractApi)
        {
            this.ModelLayer = modelAbstractApi;
            this.LogicLayer = logicAbstractApi;
            StartCommand = new RelayCommand(ChangeSize);
            StopCommand = new RelayCommand(Stop);
            canvas = new Canvas();
            canvas.HorizontalAlignment=HorizontalAlignment.Center;
            canvas.VerticalAlignment=VerticalAlignment.Top;
            canvas.Width = 600;
            canvas.Height = 480;
            canvas.Background = new SolidColorBrush(Color.FromRgb(241, 237, 237));
            
        }

        public ICommand StartCommand
        { get; set; }
        public ICommand StopCommand
        { get; set; }
        public double newXPosition
        {
            get { return xPosition; }
            set
            {
                xPosition = value;
                RaisePropertyChanged();
            }
        }
       
        public void addBalls(int ballVal)
        {
           
        }
   


        public double newYPosition
        {
            get { return yPosition; }
            set
            {
                yPosition = value;
                RaisePropertyChanged();
            }
        }
        public int BallVal
        {
            get { return _BallVal; }
            set
            {
                _BallVal = value;
                RaisePropertyChanged();
            }
        }

        private void ChangeSize()
        {   

            Start();
            

                
        }
        private void Stop()
        {
            
             Move();
            
        }
        private void Start()
        {   
            LogicLayer.CreateBallsList(BallVal,balls);
            
            for (int i = balls.Count - BallVal; i < balls.Count; i++)
            {
                Ellipse ellipse = new Ellipse {Width = balls[i].Size, Height = balls[i].Size, Fill = Brushes.Black };
                Canvas.SetLeft(ellipse, balls[i].X);
                Canvas.SetTop(ellipse, balls[i].Y);
                ellipseCollection.Add(ellipse);
                canvas.Children.Add(ellipse);
            }

        }

        private void Move()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].newPosition(600, 480);
                Canvas.SetLeft(ellipseCollection[i], balls[i].X);
                Canvas.SetTop(ellipseCollection[i], balls[i].Y);
            }
            
        }

        public IList<Logic.Ball> CirclesCollection
        {
            get
            {
                return b_CirclesCollection;
            }
            set
            {
                if (value.Equals(b_CirclesCollection))
                    return;
                RaisePropertyChanged("CirclesCollection");
            }
        }

    }
}
