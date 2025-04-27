using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    internal interface IFileStorage
    {
        void saveDictionary(IDictionary dictionary, string filePath);
        Dictionary loadDictionary(string filePath);
        void exportTranslations(string word, string[] translations, string exportPath);
    }
}
