using System;
using System.Collections.Generic;

namespace KMFService.Core
{
    public interface ICurrencyManager
    {
        void SaveList(IList<Currency> currencies);
        Currency Get(in DateTime dateOn, string code);

    }
}
