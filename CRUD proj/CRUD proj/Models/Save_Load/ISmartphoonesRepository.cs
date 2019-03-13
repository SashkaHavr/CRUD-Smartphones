using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace CRUD_proj.Models.Save_Load
{
    interface ISmartphoonesRepository
    {
        void Load();
        void Save();
        IEnumerable<Smartphone> GetAll();
        Smartphone Get(int id);
        int Add(Smartphone smartphone);
        void Update(Smartphone smartphone);
        void Delete(int id);
    }
}
