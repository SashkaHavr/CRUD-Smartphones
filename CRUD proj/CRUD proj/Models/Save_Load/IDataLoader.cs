using System.Collections.Generic;

namespace CRUD_proj.Models.Save_Load
{
    interface IDataLoader
    {
        IEnumerable<Smartphone> Load();
    }
}
