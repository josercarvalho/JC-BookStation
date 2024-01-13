using System;
using System.Globalization;

namespace JC_BookStation.Aplicacao
{
    /// <summary>
    /// Métodos utilitários
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveNaoNumericos(string text)
        {
            var reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool ValidaCpf(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            var numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString(CultureInfo.InvariantCulture));

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                    return false;

            return true;
        }

        //Método que valida o CNPJ 
        /// <summary>
        /// Valida se um cnpj é válido
        /// </summary>
        /// <param name="vrCNPJ"></param>
        /// <returns></returns>
        public static bool ValidaCNPJ(string vrCNPJ)
        {
            if (vrCNPJ == null) throw new ArgumentNullException("vrCNPJ");

            string cnpj = vrCNPJ.Replace(".", "");
            cnpj = cnpj.Replace("/", "");
            cnpj = cnpj.Replace("-", "");

            const string ftmt = "6543298765432";
            var digitos = new int[14];
            var soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            var resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            var cnpjOk = new bool[2];
            cnpjOk[0] = false;
            cnpjOk[1] = false;

            try
            {
                int nrDig;
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                     cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        cnpjOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        cnpjOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }

                return (cnpjOk[0] && cnpjOk[1]);

            }
            catch
            {
                return false;
            }

        }

        //Método que valida o Cep
        /// <summary>
        /// Valida se um cep é válido
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        public static bool ValidaCep(string cep)
        {
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
                //txt.Text = cep;
            }
            return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }

        //Método que valida o Email
        public static bool ValidaEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, ("(?<user>[^@]+)@(?<host>.+)"));
        }

        //public static MvcHtmlString TypeaheadFor<TModel, TValue>(
        //this HtmlHelper<TModel> htmlHelper,
        //Expression<Func<TModel, TValue>> expression,
        //IEnumerable<string> source,
        //int items = 8)
        //{
        //    if (expression == null)
        //        throw new ArgumentNullException("expression");

        //    if (source == null)
        //        throw new ArgumentNullException("source");

        //    var jsonString = new JavaScriptSerializer().Serialize(source);

        //    return htmlHelper.TextBoxFor(
        //        expression,
        //        new
        //        {
        //            autocomplete = "off",
        //            data_provide = "typeahead",
        //            data_items = items,
        //            data_source = jsonString
        //        }
        //    );
        //}
    }
}