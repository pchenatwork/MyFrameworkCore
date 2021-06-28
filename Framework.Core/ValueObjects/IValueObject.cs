﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.ValueObjects
{
    #region	Header
    ///	<summary>
    ///	https://enterprisecraftsmanship.com/2015/04/13/dto-vs-value-object-vs-poco/
    ///	ValueObject == Domain Model. DO contain logic
    ///	NOT ORM Entities 
    ///	</summary>
    #endregion Header
    public interface IValueObject
    {
        #region	Public Methods
        //	*************************************************************************
        //				   Public methods
        //	*************************************************************************

        /// <summary>
        /// Copy all the member variables from the source object.
        /// Call base.CopyFrom first in the implementation.
        /// </summary>
        /// <param name="source">The source object.</param>
        void CopyFrom(IValueObject source);
        /// <summary>
        /// Serializes the value object into a string.
        /// </summary>
        /// <returns></returns>
        String ToString();

        #endregion Public Methods

        #region	Properties
        //	*************************************************************************
        //				   Properties
        //	*************************************************************************
        /// <summary>
        /// Id property - every value object should have an identity.
        /// </summary>
        int Id
        {
            get;
            set;
        }

        /// <summary>
        /// CreateUser property
        /// </summary>
        string CreateBy
        {
            get;
            set;
        }

        /// <summary>
        /// CreateDate property
        /// </summary>
        DateTime CreateDate
        {
            get;
            set;
        }

        /// <summary>
        /// ChangeUser property
        /// </summary>
        string UpdatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// ChangeDate property
        /// </summary>
        DateTime UpdatedDate
        {
            get;
            set;
        }

        /// <summary>
        /// property for extra meta data 
        /// </summary>
        string Extra
        {
            get;
            set;
        }

        int TotalRecordNumber { get; set; }
        string Result { get; set; }
        #endregion Properties
    } 
}