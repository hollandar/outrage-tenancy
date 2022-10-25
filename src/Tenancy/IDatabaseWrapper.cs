using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy
{
    public interface IDatabaseWrapper : IDisposable
    {
        bool EstablishDatabase(string databaseName, string username, string password);
    }
}
