using System;
using System.Collections.Generic;
using System.Text;

namespace AppBase.Core.Interfaces
{
    ///	<summary>
    ///	ValueObject == Domain Model. DO contain logic
    ///	NOT ORM Entities 
    ///	</summary>
    ///	https://enterprisecraftsmanship.com/2015/04/13/dto-vs-value-object-vs-poco/
    public interface IValueObject 
    {
        int Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string UpdatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        string Extra { get; set; }
        void CopyFrom(IValueObject source);
    }
}
