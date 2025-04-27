using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    internal class DictionaryFactory
    {
        public Dictionary create(string languageFrom, string languageTo)
        {
            return new Dictionary(languageFrom, languageTo);
        }
    }
}
