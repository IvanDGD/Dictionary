using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    internal interface IMenu
    {
        void showMainMenu();
        void showDictionaryMenu(IDictionary dictionary);
        void navigateBack();
    }
}
