using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    
    public class MainWindowViewModel : BaseViewModel
    {
        private ModelAbstractApi ModelLayer = ModelAbstractApi.CreateApi();
        private IList<object> b_CirclesCollection;

        public MainWindowViewModel() : this(ModelAbstractApi.CreateApi())
        {
        }

        public MainWindowViewModel(ModelAbstractApi modelAbstractApi)
        {
            this.ModelLayer = modelAbstractApi;
        }

        public IList<object> CirclesCollection
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
