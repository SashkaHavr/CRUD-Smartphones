using Newtonsoft.Json;
using System.IO;

namespace CRUD_proj.Models.Save_Load
{
    class JSONWindoSettingsSaver : IWindowSettingsSaver
    {
        public void Save(WindowSettings windowSettings)
        {
            string serialized = JsonConvert.SerializeObject(windowSettings, Formatting.Indented);
            File.WriteAllText("WindowSettings.json", serialized);
        }
    }
}
