using Newtonsoft.Json;
using System.IO;

namespace CRUD_proj.Models.Save_Load
{
    class JSOMWindowSettingsLoader : IWindowSettingsLoader
    {
        public WindowSettings Load()
        {
            if (File.Exists("WindowSettings.json"))
            {
                string str = File.ReadAllText("WindowSettings.json");
                return JsonConvert.DeserializeObject<WindowSettings>(str);
            }
            return new WindowSettings();
        }
    }
}
