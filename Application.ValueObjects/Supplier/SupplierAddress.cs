using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ValueObjects.Supplier
{

    [Serializable]
    public class SupplierAddress : AbstractValueObject<SupplierAddress>
    {
        protected override void _CopyFrom(SupplierAddress source)
        {
            throw new NotImplementedException();
        }

        protected override bool _Equals(SupplierAddress that)
        {
            throw new NotImplementedException();
        }

        protected override int _GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
