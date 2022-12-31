﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetShop.Attributes
{
    public class NoDigitsAttribute : ValidationAttribute
    {
        public NoDigitsAttribute() : base("No numbers allowed!")
         { 
        
        }

        public override bool IsValid(object value)
        {
            string strValue = Convert.ToString(value);
            bool result = true;
            foreach (char c in strValue.ToCharArray())
            {
                if (c >= '0' && c <= '9') result = false;
            }
            return result;
        }
    }
}