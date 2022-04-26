using System;
using System.Collections.Generic;
using System.Text;
using Logic;
using System.Windows;



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
