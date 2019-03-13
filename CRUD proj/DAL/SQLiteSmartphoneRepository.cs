using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace DAL
{
    public class SQLiteSmartphoneRepository
    {
        SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("select * from Smartphone", "Data Source=Smartphones.db");
        DataSet dataSet = new DataSet();
        const string createQuery = @"CREATE TABLE Smartphone
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
        public SQLiteSmartphoneRepository()
        {
            if(!File.Exists("Smartphones.db"))
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=Smartphones.db"))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();
                }
            }
            SQLiteCommandBuilder builder = new SQLiteCommandBuilder(dataAdapter);
        }

        public void Load()
        {
            dataSet.Clear();
            dataAdapter.Fill(dataSet);
            dataSet.Tables[0].Columns["SmartphoneId"].AllowDBNull = false;
        }

        public void Save() => dataAdapter.Update(dataSet);

        public IEnumerable<Smartphone> GetAll()
        {
            return from smartphone in dataSet.Tables[0].AsEnumerable()
                   select new Smartphone
                   {
                       Title = smartphone.Field<string>("Title"),
                       Price = smartphone.Field<double>("Price").ToString(),
                       PhotoPath = smartphone.Field<string>("PhotoPath").SetPathWithoutRoot(),
                       Manufacturer = smartphone.Field<string>("Manufacturer"),
                       DisplaySize = smartphone.Field<double>("DisplaySize").ToString(),
                       Resolution = new Resolution
                       {
                           Horizontal = (int)smartphone.Field<long>("ResolutionHorizontal"),
                           Vertical = (int)smartphone.Field<long>("ResolutionVertical")
                       },
                       Ram = (int)smartphone.Field<long>("Ram"),
                       Rom = (int)smartphone.Field<long>("Rom"),
                       CpuSpeed = smartphone.Field<double>("CpuSpeed").ToString(),
                       CpuType = smartphone.Field<string>("CpuType"),
                       RearCameraRes = smartphone.Field<double>("RearCameraRes").ToString(),
                       FrontCameraRes = smartphone.Field<double>("FrontCameraRes").ToString(),
                       Nfc = smartphone.Field<bool?>("Nfc"),
                       ThreePointFiveJack = smartphone.Field<bool?>("ThreePointFiveJack"),
                       Os = smartphone.Field<string>("Os"),
                       BatteryCapacity = (int)smartphone.Field<long>("BatteryCapacity"),
                       Connection = smartphone.Field<string>("Connection"),
                       Id = (int)smartphone.Field<long>("SmartphoneID")
                   };
        }

        public Smartphone Get(int id)
        {
            return (from smartphone in GetAll()
                   where smartphone.Id == id
                   select smartphone).ToList()[0];
        }

        void SetRow(Smartphone smartphone, DataRow row)
        {
            row.SetField("Title", smartphone.Title);
            row.SetField("Price", double.Parse(smartphone.Price));
            row.SetField("PhotoPath", smartphone.PhotoPath);
            row.SetField("Manufacturer", smartphone.Manufacturer);
            row.SetField("DisplaySize", double.Parse(smartphone.DisplaySize));
            row.SetField("ResolutionHorizontal", smartphone.Resolution.Horizontal);
            row.SetField("ResolutionVertical", smartphone.Resolution.Vertical);
            row.SetField("Ram", smartphone.Ram);
            row.SetField("Rom", smartphone.Rom);
            row.SetField("CpuSpeed", double.Parse(smartphone.CpuSpeed));
            row.SetField("CpuType", smartphone.CpuType);
            row.SetField("RearCameraRes", smartphone.RearCameraRes);
            row.SetField("FrontCameraRes", smartphone.FrontCameraRes);
            row.SetField("Nfc", smartphone.Nfc);
            row.SetField("ThreePointFiveJack", smartphone.ThreePointFiveJack);
            row.SetField("Os", smartphone.Os);
            row.SetField("BatteryCapacity", smartphone.BatteryCapacity);
            row.SetField("Connection", smartphone.Connection);
        }

        int GetNextId()
        {
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                long max = dataSet.Tables[0].Rows[0].Field<long>("SmartphoneId");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    long cur = row.Field<long>("SmartphoneId");
                    if (cur > max)
                        max = cur;
                }
                return (int)max + 1;
            }
            else
                return 1;
        }

        public int Add(Smartphone smartphone)
        {
            var row = dataSet.Tables[0].NewRow();
            SetRow(smartphone, row);
            row.SetField("SmartphoneId", GetNextId());
            dataSet.Tables[0].Rows.Add(row);
            return (int)row.Field<long>("SmartphoneId");
        }

        public void Update(Smartphone smartphone)
        {
            DataRow uRow = null;
            foreach (DataRow row in dataSet.Tables[0].Rows)
                if (row.Field<long>("SmartphoneID") == smartphone.Id)
                {
                    uRow = row;
                    break;
                }
            SetRow(smartphone, uRow);
        }

        public void Delete(int id)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
                if (row.Field<long>("SmartphoneID") == id)
                {
                    dataSet.Tables[0].Rows.Remove(row);
                    break;
                }
        }
    }
}
