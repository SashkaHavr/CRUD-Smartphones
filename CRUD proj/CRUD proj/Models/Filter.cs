using DAL;
using CRUD_proj.Models.Structs;
using LinqKit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace CRUD_proj.Models
{
    class Filter : NotifiedModelBase
    {
        ExpressionStarter<Smartphone> predicate;
        ObservableCollection<Smartphone> smartphones;

        #region Filter Fields
        FromToInteger ram;
        FromToInteger rom;
        FromToInteger batteryCapacity;
        FromToDoubleString displaySize;
        FromToDoubleString cpuSpeed;
        FromToDoubleString price;
        FromToDoubleString frontCameraResolution;
        FromToDoubleString rearCameraResolution;
        string selectedManufacurer;
        string selectedResolution;
        string selectedCpuType;
        string selectedOs;
        string selectedConnection;
        bool? nfc;
        bool? threePointFiveJack;
        ObservableCollection<string> manufacturers;
        ObservableCollection<string> oss;
        ObservableCollection<string> connections;
        ObservableCollection<string> cpuTypes;
        ObservableCollection<string> resolutions;
        string searchText = string.Empty;
        #endregion

        public IList<Smartphone> Smartphones
        {
            get => smartphones;
            set
            {
                smartphones = new ObservableCollection<Smartphone>(value);
                smartphones.CollectionChanged += CollectionChangedHandler;
                ResetFields();
                foreach (var smartphone in smartphones)
                    AddFilterUpdate(smartphone);
            }
        }

        #region Filter Propertires
        public int RamFrom { get => ram.From; set { ram.From = value; Notify(); } }
        public int RamTo { get => ram.To; set { ram.To = value; Notify(); } }
        public int RomFrom { get => rom.From; set { rom.From = value; Notify(); } }
        public int RomTo { get => rom.To; set { rom.To = value; Notify(); } }
        public int BatteryCapacityFrom { get => batteryCapacity.From; set { batteryCapacity.From = value; Notify(); } }
        public int BatteryCapacityTo { get => batteryCapacity.To; set { batteryCapacity.To = value; Notify(); } }
        public string DisplaySizeFrom { get => displaySize.From; set { displaySize.From = value; Notify(); } }
        public string DisplaySizeTo { get => displaySize.To; set { displaySize.To = value; Notify(); } }
        public string CpuSpeedFrom { get => cpuSpeed.From; set { cpuSpeed.From = value; Notify(); } }
        public string CpuSpeedTo { get => cpuSpeed.To; set { cpuSpeed.To = value; Notify(); } }
        public string PriceFrom { get => price.From; set { price.From = value; Notify(); } }
        public string PriceTo { get => price.To; set { price.To = value; Notify(); } }
        public string FrontCameraResFrom { get => frontCameraResolution.From; set {frontCameraResolution.From = value; Notify(); } }
        public string FrontCameraResTo { get => frontCameraResolution.To; set { frontCameraResolution.To = value; Notify(); } } 
        public string RearCameraResFrom { get => rearCameraResolution.From; set { rearCameraResolution.From = value; Notify(); } }
        public string RearCameraResTo { get => rearCameraResolution.To; set { rearCameraResolution.To = value; Notify(); } }
        public ObservableCollection<string> Manufacturers { get => manufacturers; set { manufacturers = value; Notify(); } }
        public ObservableCollection<string> Resolutions { get => resolutions; set { resolutions = value; Notify(); } }
        public ObservableCollection<string> CpuTypes { get => cpuTypes; set { cpuTypes = value; Notify(); } }
        public ObservableCollection<string> Oss { get => oss; set { oss = value; Notify(); } }
        public ObservableCollection<string> Connections { get => connections; set { connections = value; Notify(); } }
        public string SelectedManufacturer { get => selectedManufacurer; set { selectedManufacurer = value; Notify(); } }
        public string SelectedResolution { get => selectedResolution; set { selectedResolution = value; Notify(); } }
        public string SelectedCpuType { get => selectedCpuType; set { selectedCpuType = value; Notify(); } }
        public string SelectedOs { get => selectedOs; set { selectedOs = value; Notify(); } }
        public string SelectedConnection { get => selectedConnection; set { selectedConnection = value; Notify(); } }
        public bool? Nfc { get => nfc; set { nfc = value; Notify(); } }
        public bool? ThreePointFiveJack { get => threePointFiveJack; set { threePointFiveJack = value; Notify(); } }
        public bool Update { get; private set; } = false;
        public string SearchText
        {
            get => searchText;
            set
            {
                if(value.Length <= 20)
                    searchText = value;
                Notify();
            }
        }
        #endregion

        public IEnumerable<Smartphone> GetFiltered() => Smartphones.Where(predicate);

        public Filter()
        {
            predicate = PredicateBuilder.New<Smartphone>(true);
            predicate.And(x => DSNSBigger(x.Price, PriceFrom) && DSNSSmaller(x.Price, PriceTo));
            predicate.And(x => DSNSBigger(x.DisplaySize, DisplaySizeFrom) && DSNSSmaller(x.DisplaySize, DisplaySizeTo));
            predicate.And(x => DSNSBigger(x.CpuSpeed, CpuSpeedFrom) && DSNSSmaller(x.CpuSpeed, CpuSpeedTo));
            predicate.And(x => DSNSBigger(x.FrontCameraRes, FrontCameraResFrom) && DSNSSmaller(x.FrontCameraRes, FrontCameraResTo));
            predicate.And(x => DSNSBigger(x.RearCameraRes, RearCameraResFrom) && DSNSSmaller(x.RearCameraRes, RearCameraResTo));
            predicate.And(x => x.BatteryCapacity >= BatteryCapacityFrom && x.BatteryCapacity <= BatteryCapacityTo);
            predicate.And(x => x.Ram >= RamFrom && x.Ram <= RamTo);
            predicate.And(x => x.Rom >= RomFrom && x.Rom <= RomTo);
            predicate.And(x => SelectedResolution == "-" || x.Resolution.ToString() == SelectedResolution);
            predicate.And(x => SelectedManufacturer == "-" || x.Manufacturer == SelectedManufacturer);
            predicate.And(x => SelectedOs == "-" || x.Os == SelectedOs);
            predicate.And(x => SelectedCpuType == "-" || x.CpuType == SelectedCpuType);
            predicate.And(x => SelectedConnection == "-" || x.Connection == SelectedConnection);
            predicate.And(x => nfc.HasValue == false || x.Nfc == Nfc);
            predicate.And(x => threePointFiveJack.HasValue == false || x.ThreePointFiveJack == ThreePointFiveJack);
            predicate.And(x => searchText == string.Empty ? true : x.Title.ToLower().Contains(searchText.ToLower()));
        }

        #region Resets
        void ResetFields()
        {
            Update = true;
            ram = new FromToInteger();
            rom = new FromToInteger();
            batteryCapacity = new FromToInteger();
            displaySize = new FromToDoubleString();
            cpuSpeed = new FromToDoubleString();
            price = new FromToDoubleString();
            frontCameraResolution = new FromToDoubleString();
            rearCameraResolution = new FromToDoubleString();
            Manufacturers = new ObservableCollection<string>() { "-" };
            Oss = new ObservableCollection<string>() { "-" };
            Connections = new ObservableCollection<string>() { "-" };
            CpuTypes = new ObservableCollection<string>() { "-" };
            Resolutions = new ObservableCollection<string>() { "-" };
            Nfc = ThreePointFiveJack = null;
            SelectedConnection = SelectedCpuType = SelectedManufacturer = SelectedOs = SelectedOs = SelectedResolution = "-";
            SearchText = string.Empty;
            Update = false;
        }


        public void Reset()
        {
            Update = true;
            price.Reset();
            displaySize.Reset();
            cpuSpeed.Reset();
            frontCameraResolution.Reset();
            rearCameraResolution.Reset();
            batteryCapacity.Reset();
            ram.Reset();
            rom.Reset();
            SelectedConnection = SelectedCpuType = SelectedManufacturer = SelectedOs = SelectedOs = SelectedResolution = "-";
            NotifyFromTo();
            ThreePointFiveJack = null;
            SearchText = string.Empty;
            Update = false;
            Nfc = null;
        }
        #endregion

        #region Filter Updates
        void AddFilterUpdate(Smartphone smartphone)
        {
            Update = true;
            FromToUpdateFilter(ref price, smartphone.Price);
            FromToUpdateFilter(ref displaySize, smartphone.DisplaySize);
            FromToUpdateFilter(ref cpuSpeed, smartphone.CpuSpeed);
            FromToUpdateFilter(ref frontCameraResolution, smartphone.FrontCameraRes);
            FromToUpdateFilter(ref rearCameraResolution, smartphone.RearCameraRes);
            FromToUpdateFilter(ref batteryCapacity, smartphone.BatteryCapacity);
            FromToUpdateFilter(ref ram, smartphone.Ram);
            FromToUpdateFilter(ref rom, smartphone.Rom);
            ObservableUpdate(Manufacturers, smartphone.Manufacturer);
            ObservableUpdate(Resolutions, smartphone.Resolution.ToString());
            ObservableUpdate(CpuTypes, smartphone.CpuType);
            ObservableUpdate(Oss, smartphone.Os);
            ObservableUpdate(Connections, smartphone.Connection);
            NotifyFromTo();
            Update = false;
        }

        public void EditFilterUpdate(Smartphone smartphone, Smartphone oldSmartphone)
        {
            RemoveFilterUpdate(oldSmartphone);
            AddFilterUpdate(smartphone);
        }

        void RemoveFilterUpdate(Smartphone smartphone)
        {
            Update = true;
            FromToRemoveUpdateFilter(ref price, "Price", smartphone);
            FromToRemoveUpdateFilter(ref displaySize, "DisplaySize", smartphone);
            FromToRemoveUpdateFilter(ref cpuSpeed, "CpuSpeed", smartphone);
            FromToRemoveUpdateFilter(ref frontCameraResolution, "FrontCameraRes", smartphone);
            FromToRemoveUpdateFilter(ref rearCameraResolution, "RearCameraRes", smartphone);
            FromToRemoveUpdateFilter(ref batteryCapacity, "BatteryCapacity", smartphone);
            FromToRemoveUpdateFilter(ref ram, "Ram", smartphone);
            FromToRemoveUpdateFilter(ref rom, "Rom", smartphone);
            TryRemoveFilter(smartphone, "Manufacturer", Manufacturers);
            TryRemoveFilter(smartphone, "CpuType", CpuTypes);
            TryRemoveFilter(smartphone, "Connection", Connections);
            TryRemoveFilter(smartphone, "Os", Oss);
            TryRemoveFilter(smartphone, "Resolution", Resolutions);
            NotifyFromTo();
            Update = false;
        }
        #endregion

        #region HelpFilterUpdates
        void TryRemoveFilter(Smartphone smartphone, string propName, ObservableCollection<string> list)
        {
            var prop = typeof(Smartphone).GetProperty(propName);
            if (CanRemoveFilter(smartphone, propName))
                list.Remove(prop.GetValue(smartphone).ToString());
        }

        bool CanRemoveFilter(Smartphone smartphone, string propName)
        {
            var prop = typeof(Smartphone).GetProperty(propName);
            foreach (var i in Smartphones)
                if (prop.GetValue(i).ToString() == prop.GetValue(smartphone).ToString())
                    return false;
             return true;
        }

        void FromToUpdateFilter(ref FromToDoubleString fromToDoubleString, string param)
        {
            if (param != null)
            {
                if (fromToDoubleString.MinFrom == null || DSNSSmaller(param, fromToDoubleString.MinFrom))
                    fromToDoubleString.MinFrom = param;
                if (fromToDoubleString.MaxTo == null || DSNSBigger(param, fromToDoubleString.MaxTo))
                    fromToDoubleString.MaxTo = param;
            }
        }

        void FromToUpdateFilter(ref FromToInteger fromToInteger, int param)
        {
            if (fromToInteger.IsMinNotNull == false || param <= fromToInteger.MinFrom)
            {
                fromToInteger.MinFrom = param;
                if (fromToInteger.IsMinNotNull == false)
                    fromToInteger.IsMinNotNull = true;
            }
            if (param >= fromToInteger.MaxTo)
                fromToInteger.MaxTo = param;
        }

        void FromToRemoveUpdateFilter(ref FromToDoubleString fromToDoubleString, string propName, Smartphone removedSmartphone)
        {
            var prop = typeof(Smartphone).GetProperty(propName);
            if (prop.GetValue(removedSmartphone).ToString() == fromToDoubleString.MaxTo || prop.GetValue(removedSmartphone).ToString() == fromToDoubleString.MinFrom)
                if (Smartphones.Count > 0)
                {
                    fromToDoubleString.MinFrom = fromToDoubleString.MaxTo = prop.GetValue(Smartphones[0]).ToString();
                    foreach (var smartphone in Smartphones)
                    {
                        string val = prop.GetValue(smartphone).ToString();
                        if (DSNSSmaller(val, fromToDoubleString.MinFrom))
                            fromToDoubleString.MinFrom = val;
                        if (DSNSBigger(val, fromToDoubleString.MaxTo))
                            fromToDoubleString.MaxTo = val;
                    }
                }
                else
                {
                    fromToDoubleString.MinFrom = "0";
                    fromToDoubleString.MaxTo = "0";
                }
        }

        void FromToRemoveUpdateFilter(ref FromToInteger fromToInteger, string propName, Smartphone removedSmartphone)
        {
            var prop = typeof(Smartphone).GetProperty(propName);
            if ((int)prop.GetValue(removedSmartphone) == fromToInteger.MaxTo || (int)prop.GetValue(removedSmartphone) == fromToInteger.MinFrom)
                if (Smartphones.Count > 0)
                {
                    fromToInteger.MinFrom = fromToInteger.MaxTo = (int)prop.GetValue(Smartphones[0]);
                    foreach (var smartphone in Smartphones)
                    {
                        int val = (int)prop.GetValue(smartphone);
                        if (val <= fromToInteger.MinFrom)
                            fromToInteger.MinFrom = val;
                        if (val >= fromToInteger.MaxTo)
                            fromToInteger.MaxTo = val;
                    }
                }
                else
                {
                    fromToInteger.MinFrom = 0;
                    fromToInteger.MaxTo = 0;
                }
        }


        void ObservableUpdate(ObservableCollection<string> obs, string param)
        {
                if (param != string.Empty && !obs.Contains(param))
                    obs.Add(param);
        }

        bool DSNSBigger(string x, string y) => double.Parse(x) >= double.Parse(y);

        bool DSNSSmaller(string x, string y) => double.Parse(x) <= double.Parse(y);

        void NotifyFromTo()
        {
            foreach (var prop in typeof(Filter).GetProperties())
                if (prop.Name.Contains("To") || prop.Name.Contains("From"))
                    Notify(prop.Name);
        }
        #endregion

        void CollectionChangedHandler(object o, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var i in e.NewItems)
                    AddFilterUpdate(i as Smartphone);
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var i in e.OldItems)
                    RemoveFilterUpdate(i as Smartphone);
        }
    }
}
