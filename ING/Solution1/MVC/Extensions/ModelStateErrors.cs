﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace MVC.Extensions
{
    public static class ModelStateErrors
    {
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errorsList = (from m in modelState
                                  where m.Value.Errors.Any()
                                  select new
                                  {
                                      fieldName = m.Key,
                                      errorList = (from msg in m.Value.Errors
                                                   select msg.ErrorMessage).ToArray()
                                  })
                         .AsEnumerable()
                         .ToDictionary(v => v.fieldName, v => v);
                return errorsList;
            }
            return null;
        }
    }

    public static class Proba
    {
        public static string dodajA(this string s)
        {
            s += "A";
            return s;
        }
    }

}