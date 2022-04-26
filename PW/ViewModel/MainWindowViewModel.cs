using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;


namespace ViewModel
{
    
    public class MainWindowViewModel : BaseViewModel
    {
        private ModelAbstractApi ModelLayer = ModelAbstractApi.CreateApi();
        private IList<Logic.Ball> b_CirclesCollection;
        private List<Logic.Ball> b_BallsCollection;
        private int _BallVal;
        private double xPosition;
        private double yPosition;
        Logic.Ball ball = new Logic.Ball(50, 200, 200, 10, 10);
        public MainWindowViewModel() : this(ModelAbstractApi.CreateApi())
        {
        }

        public MainWindowViewModel(ModelAbstractApi modelAbstractApi)
        {
            this.ModelLayer = modelAbstractApi;
            _BallVal = ball.Size;
            xPosition = (double)ball.X;
            yPosition = (double)ball.Y;
            StartCommand = new RelayCommand(ChangeSize);    
        }

        public ICommand StartCommand
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
            ball.newPosition(600, 480);
            newXPosition = (double)ball.X;
            newYPosition = (double)ball.Y;
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
