
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
        public static ModelAbstractApi CreateApi()
        {
            return new ModelApi();
        }
    }
    internal class ModelApi : ModelAbstractApi
    { 
        private LogicAbstractApi LogicLayer = LogicAbstractApi.CreateApi(800,800);
        
    

    
     
    }

}
