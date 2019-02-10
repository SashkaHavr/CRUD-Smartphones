using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Threading;

namespace CRUD_proj.Models.Save_Load
{
    class DBDataSaver : IDataSaver
    {
        string CreateTableQuery()
        {
            return @"CREATE TABLE Smartphone
                    (
                        SmartphoneID INTEGER       NOT NULL
                                                   PRIMARY KEY AUTOINCREMENT
                                                   UNIQUE,
                        Title VARCHAR (100) NOT NULL,
                        Price        double NOT NULL,
                        PhotoPath VARCHAR(200) NOT NULL,
                        Manufacturer varchar(40),
                    	DisplaySize double,
                        ResolutionVertical integer,
                    	ResolutionHorizontal integer,
                        Ram integer,
                    	Rom integer,
                        CpuSpeed double,
                        CpuType varchar(40),
                    	RearCameraRes double,
                        FrontCameraRes double,
                        Nfc boolean,
                    	ThreePointFiveJack boolean,
                        Os varchar(40),
                    	BatteryCapacity integer,
                        Connection varchar(40)
                    );";

        }

        string InsertQuery(Smartphone smartphone)
        {
            string tp5j = smartphone.ThreePointFiveJack == null ? "null" : smartphone.ThreePointFiveJack.ToString();
            string nfc = smartphone.Nfc == null ? "null" : smartphone.Nfc.ToString();
            smartphone.PhotoPath = smartphone.PhotoPath.LeftPathWithoutCurPath();
            return $@"insert into Smartphone
                    (Title,Price, PhotoPath, Manufacturer, DisplaySize, ResolutionVertical, ResolutionHorizontal, Ram,
                    Rom, CpuSpeed, CpuType, RearCameraRes, FrontCameraRes, Nfc, ThreePointFiveJack, Os, BatteryCapacity, Connection)
                     values ('{smartphone.Title}', {double.Parse(smartphone.Price).ToString()}, '{smartphone.PhotoPath}', '{smartphone.Manufacturer}',
                    {double.Parse(smartphone.DisplaySize).ToString()}, {smartphone.Resolution.Vertical.ToString()},
                    {smartphone.Resolution.Horizontal.ToString()}, {smartphone.Ram.ToString()},
                    {smartphone.Rom.ToString()}, {double.Parse(smartphone.CpuSpeed).ToString()}, '{smartphone.CpuType}',
                    {double.Parse(smartphone.RearCameraRes).ToString()}, {double.Parse(smartphone.FrontCameraRes).ToString()},
                    {nfc}, {tp5j}, '{smartphone.Os}', {smartphone.BatteryCapacity.ToString()}, '{smartphone.Connection}');";
        }

        public void Save(IEnumerable<Smartphone> smartphones)
        {
            if (File.Exists("Smartphones.db"))
                File.Delete("Smartphones.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Smartphones.db"))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(CreateTableQuery(), connection);
                cmd.ExecuteNonQuery();
                CultureInfo dotCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentCulture = dotCulture;
                Thread.CurrentThread.CurrentUICulture = dotCulture;
                foreach (var smartphone in smartphones)
                {
                    cmd.CommandText = InsertQuery(smartphone);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
