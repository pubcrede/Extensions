//-----------------------------------------------------------------------
// <copyright file="MvcController.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extensions;
using System;
using System.Web.Mvc;

namespace Genesys.Extras.Web.Http
{
    /// <summary>
    /// Overrides the MVC DefaultModelBinder
    ///    You’ll need to add the following to the Global.asax Application_Start method to use your custom model binder.
    ///     C#: ModelBinders.Binders.Add(typeof(string), new MvcModelBinder());
    /// </summary>
    /// <remarks></remarks>
    public class MvcModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// Binds model to view. 
        /// typeof(string): Null-safe binding for strings, posted models will contain empty string instead of null
        /// </summary>
        /// <param name="controllerContext">Context of controller</param>
        /// <param name="bindingContext">Context of view model binding</param>
        /// <returns>typeof(string): Returns empty string instead of null</returns>
        /// <remarks></remarks>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            dynamic value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            object returnValue = null;
            
            if (value == null == false)
            {
                returnValue = value.AttemptedValue;
                if (bindingContext.ModelType == typeof(string))
                {
                    bindingContext.ModelMetadata.ConvertEmptyStringToNull = false;
                    if (string.IsNullOrEmpty(value.AttemptedValue))
                    {
                        returnValue = TypeExtension.DefaultString;
                    }
                } 
            }

            return returnValue;
        }
    }
}
