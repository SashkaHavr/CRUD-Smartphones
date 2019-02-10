using System.Collections.Generic;

namespace CRUD_proj.Models.Save_Load
{
    interface IDataSaver
    {
        void Save(IEnumerable<Smartphone> smartphones);
    }
}
