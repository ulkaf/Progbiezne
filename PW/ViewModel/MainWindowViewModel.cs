using Model;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModel
{

    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ModelAbstractApi ModelLayer;
        private int _BallVal;
        private IList _balls;
        public ICommand AddCommand { get; set; }

        public MainWindowViewModel()
        {

            ModelLayer = ModelAbstractApi.CreateApi(600, 480);
            StopCommand = new RelayCommand(Stop);
            AddCommand = new RelayCommand(CreateEllipses);

        }


        public ICommand StopCommand
        { get; set; }


        public int BallVal
        {
            get { return _BallVal; }
            set
            {
                _BallVal = value;
                RaisePropertyChanged();
            }
        }

  

        private void CreateEllipses()
        {
            Balls = ModelLayer.Start(BallVal);
            ModelLayer.StartMoving();
        }
        private void Stop()
        {

            ModelLayer.Stop();

        }
        public IList Balls
        {
            get => _balls;
            set
            {
                if (value.Equals(_balls))
                    return;
                _balls = value;
                RaisePropertyChanged(nameof(Balls));
            }
        }


    }
}
