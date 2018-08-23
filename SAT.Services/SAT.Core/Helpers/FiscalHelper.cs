using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SAT.Core.Helpers
{
    public static class FiscalHelper
    {
        public static bool IsTaxIdValid(string taxId)
        {
            bool bValidaEstructuraRFC = false;
            try
            {
                RegexOptions regexOptions = RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;

                Regex rfc1 = new Regex("^[A-Za-zñÑ&]{3}\\d{6}[A-Za-z0-9]{3}$", regexOptions);
                Regex rfc2 = new Regex("^[A-Za-zñÑ]{4}\\d{6}[A-Za-z0-9]{3}$", regexOptions);

                if (rfc1.IsMatch(taxId))
                {
                    DateTime fechaResultado;
                    DateTime.TryParseExact(taxId.Substring(3, 6), "yyMMdd", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out fechaResultado);

                    if (fechaResultado != null)
                        bValidaEstructuraRFC = true;
                }
                else if (rfc2.IsMatch(taxId))
                {
                    DateTime fechaResultado;
                    DateTime.TryParseExact(taxId.Substring(4, 6), "yyMMdd", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out fechaResultado);

                    if (fechaResultado != null)
                        bValidaEstructuraRFC = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bValidaEstructuraRFC;
        }
    }
}
