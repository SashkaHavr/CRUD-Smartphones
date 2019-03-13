using DAL;
using CRUD_proj.Models.Smartphone_IComparers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CRUD_proj.Models
{
    class Sorter : NotifiedModelBase
    {
        bool descending = false;
        IComparersGetter comparersGetter = new IComparersGetter();
        LocalizationManager localizationManager = LocalizationManager.GetLocalizationManager();
        Type localizationManagerType = typeof(LocalizationManager);
        Dictionary<string, IComparer<Smartphone>> sortOptions;
        string selectedSortOption;
        ObservableCollection<Smartphone> smartphones;

        public bool Descending { get => descending; set { descending = value; Notify(); } }
        public Dictionary<string, IComparer<Smartphone>> SortOptions{ get => sortOptions; set { sortOptions = value; Notify(); } }
        public string SelectedSortOption { get => selectedSortOption; set { selectedSortOption = value; Notify(); } }
        public ObservableCollection<Smartphone> Smartphones { get => smartphones; set { smartphones = value; Notify(); } }


        public Sorter()
        {
            GetSortOptions();
            PropertyChanged += PropertyChangedHandler;
            LocalizationManager.GetLocalizationManager().PropertyChanged += CultureChangedHandler;
        }

        void GetSortOptions()
        {
            var props = typeof(IComparersGetter).GetProperties();
            if (props.Length > 0)
            {
                Dictionary<string, IComparer<Smartphone>> res = new Dictionary<string, IComparer<Smartphone>>(props.Length);
                foreach (var prop in props)
                    res.Add(localizationManagerType.GetProperty($"{prop.Name}Text").GetValue(localizationManager).ToString(),
                        prop.GetValue(comparersGetter) as IComparer<Smartphone>);
                SortOptions = res;
                SelectedSortOption = localizationManagerType.GetProperty($"{props[0].Name}Text").GetValue(localizationManager).ToString();
            }
        }

        void PropertyChangedHandler(object o, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Descending")
                smartphones.StaticReverse();
            else if (e.PropertyName == "SelectedSortOption" || e.PropertyName == "Smartphones")
                Sort();
        }

        public void Sort()
        {
            if (selectedSortOption != null)
                smartphones.Sort(sortOptions[selectedSortOption]);
            if (descending)
                smartphones.StaticReverse();
        }

        void CultureChangedHandler(object o, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCulture")
                GetSortOptions();
        }
    }
}
