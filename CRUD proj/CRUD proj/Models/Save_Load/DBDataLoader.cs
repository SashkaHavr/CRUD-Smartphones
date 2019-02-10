using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace CRUD_proj.Models.Save_Load
{
    class DBDataLoader : IDataLoader
    {
        public IEnumerable<Smartphone> Load()
        {
            if (File.Exists("Smartphones.db"))
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=Smartphones.db"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from Smartphone;", connection);
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    List<Smartphone> res = new List<Smartphone>();
                    while (dataReader.Read())
                    {
                        res.Add(new Smartphone()
                        { Title = dataReader["Title"].ToString(),
                            Price = dataReader["Price"].ToString(),
                            PhotoPath = dataReader["PhotoPath"].ToString().SetPathWithoutRoot(),
                            Manufacturer = dataReader["Manufacturer"].ToString(),
                            DisplaySize = dataReader["DisplaySize"].ToString(),
                            Resolution = new Resolution()
                            {
                                Horizontal = int.Parse(dataReader["ResolutionHorizontal"].ToString()),
                                Vertical = int.Parse(dataReader["ResolutionVertical"].ToString()),
                            },
                            Ram = int.Parse(dataReader["Ram"].ToString()),
                            Rom = int.Parse(dataReader["Rom"].ToString()),
                            CpuSpeed = dataReader["CpuSpeed"].ToString(),
                            CpuType = dataReader["CpuType"].ToString(),
                            RearCameraRes = dataReader["RearCameraRes"].ToString(),
                            FrontCameraRes = dataReader["FrontCameraRes"].ToString(),
                            Nfc = dataReader["Nfc"].ToString() != string.Empty ? bool.Parse(dataReader["Nfc"].ToString()) : new bool?(),
                            ThreePointFiveJack = dataReader["ThreePointFiveJack"].ToString() != string.Empty ? bool.Parse(dataReader["ThreePointFiveJack"].ToString()) : new bool?(),
                            Os = dataReader["Os"].ToString(),
                            BatteryCapacity = int.Parse(dataReader["BatteryCapacity"].ToString()),
                            Connection = dataReader["Connection"].ToString()
                        });
                    } 
                    return res;
                }
            return new List<Smartphone>();
        }
    }
}
