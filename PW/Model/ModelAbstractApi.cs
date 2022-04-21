using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
