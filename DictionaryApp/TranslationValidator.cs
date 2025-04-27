using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DictionaryApp
{
    internal class TranslationValidator
    {
        public bool isValidTranslation(string translation)
        {
            return !string.IsNullOrWhiteSpace(translation);
        }

        public bool canDeleteTranslation(string[] translations)
        {
            return translations.Length > 1;
        }
    }
}
