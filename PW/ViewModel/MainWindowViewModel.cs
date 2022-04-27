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
        private ModelAbstractApi ModelLayer;
        private int _BallVal;
        public ICommand AddEllipses{ get; set; }

        public MainWindowViewModel()
        {
            
            this.ModelLayer = ModelAbstractApi.CreateApi(600, 480);
            StopCommand = new RelayCommand(Stop);
            AddEllipses = new RelayCommand(CreateEllipses);
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

        public Canvas Canvas 
        {   
            get => ModelLayer.Canvas; 
            set 
            { 
                ModelLayer.Canvas = value; 
                RaisePropertyChanged(); 
            } 
        }

        private void CreateEllipses()
        {
            ModelLayer.CreateEllipses(BallVal);
        }
        private void Stop()
        {

            ModelLayer.Move();
            
        }

       
    }
}
