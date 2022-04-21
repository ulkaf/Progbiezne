using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    
    public class MainWindowViewModel : BaseViewModel
    {
        private ModelAbstractApi ModelLayer = ModelAbstractApi.CreateApi();
        private IList<Logic.Ball> b_CirclesCollection;
        private List<Logic.Ball> b_BallsCollection;
        private int _BallVal;
        Logic.Ball ball = new Logic.Ball(100, 200, 200, 2, 2);
        public MainWindowViewModel() : this(ModelAbstractApi.CreateApi())
        {
        }

        public MainWindowViewModel(ModelAbstractApi modelAbstractApi)
        {
            this.ModelLayer = modelAbstractApi;
            _BallVal = ball.Size;
            
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
