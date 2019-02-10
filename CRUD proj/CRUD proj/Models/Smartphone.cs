using CRUD_proj.Models.Structs;
using System;
using System.IO;

namespace CRUD_proj.Models
{
    class Smartphone : NotifiedModelBase, ICloneable
    {
        string manufacturer = string.Empty;
        string title;
        DoubleString displaySize = new DoubleString();
        Resolution resolution = new Resolution();
        int ram;
        int rom;
        DoubleString cpuSpeed = new DoubleString();
        string cpuType = string.Empty;
        DoubleString rearCameraRes = new DoubleString();
        DoubleString frontCameraRes = new DoubleString();
        bool? nfc;
        bool? threePointFiveJack;
        string os = string.Empty;
        int batteryCapacity;
        string connection = string.Empty;
        DoubleString price = new DoubleString();
        string photoPath;

        public string Manufacturer { get => manufacturer; set { manufacturer = value;  Notify(); } }
        public string Title { get => title; set { title = value; Notify(); } }
        public string DisplaySize { get => displaySize.Value ?? "0"; set { displaySize.Value = value; Notify(); } }
        public int Ram { get => ram; set { ram = value; Notify(); } }
        public string CpuSpeed { get => cpuSpeed.Value ?? "0"; set { cpuSpeed.Value = value; Notify(); } }
        public string CpuType { get => cpuType; set { cpuType = value; Notify(); } }
        public string RearCameraRes { get => rearCameraRes.Value ?? "0"; set { rearCameraRes.Value = value; Notify(); } }
        public string FrontCameraRes { get => frontCameraRes.Value ?? "0"; set { frontCameraRes.Value = value; Notify(); } }
        public int Rom { get => rom; set { rom = value; Notify(); } }
        public bool? Nfc { get => nfc; set { nfc = value; Notify(); } }
        public string Os { get => os; set { os = value; Notify(); } }
        public int BatteryCapacity { get => batteryCapacity; set { batteryCapacity = value; Notify(); } }
        public string Connection { get => connection; set { connection = value; Notify(); } }
        public bool? ThreePointFiveJack { get => threePointFiveJack; set { threePointFiveJack = value; Notify(); } }
        public string Price { get => price.Value; set { price.Value = value; Notify(); } }
        public Resolution Resolution { get => resolution; set { resolution = value; Notify(); } }
        public string PhotoPath { get => photoPath; set { photoPath = value; Notify(); } }

        public object Clone() => this.MemberwiseClone();

    }
}
