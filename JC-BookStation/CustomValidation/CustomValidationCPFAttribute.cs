using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using JC_BookStation.Aplicacao;

namespace JC_BookStation.CustomValidation
{
    /// <summary>
    /// Validação customizada para CPF
    /// </summary>
    public class CustomValidationCpfAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Construtor
        /// </summary>
        //public CustomValidationCpfAttribute()
        //{

        //}
        
        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            if (value.ToString().Length == 14)
            {
                bool valido = Util.ValidaCpf(value.ToString());
                return valido;
            }
            else 
            {
                bool valido = Util.ValidaCNPJ(value.ToString());
                return valido;
            }
            
        }

        /// <summary>
        /// Validação client
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(null),
                ValidationType = "customvalidationcpf"
            };
        }
    }
}