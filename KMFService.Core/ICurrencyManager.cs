using System;
using System.Collections.Generic;

namespace KMFService.Core
{
    public interface ICurrencyManager
    {
        void SaveList(IList<Currency> currencies);
        IList<Currency> GetList(in DateTime dateOn, string code);
    }
}
