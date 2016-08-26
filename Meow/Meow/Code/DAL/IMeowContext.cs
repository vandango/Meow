using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meow.Code.Model;

namespace Meow.Code.DAL
{
    public interface IMeowContext
    {
        void AddCat(Cat cat);
        void Save();
        Cat Find(long id);
    }
}
