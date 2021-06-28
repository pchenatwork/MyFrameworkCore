//using Framework.Core.ValueObjects;
using AppBase.Core.Interfaces;
using AppBase.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ValueObjects.Supplier
{

    [Serializable]
    public class SupplierAddress : ValueObjectBase
    { 
        protected override void _CopyFrom(IValueObject source)
        {
            throw new NotImplementedException();
        }

        protected override bool _Equals(IValueObject that)
        {
            throw new NotImplementedException();
        }

        protected override int _GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
