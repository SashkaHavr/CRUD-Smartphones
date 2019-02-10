namespace CRUD_proj.Models.Smartphone_IComparers
{
    class IComparersGetter
    {
        BatteryCapacityComparer batteryCapacityComparer;
        TitleComparer titleComparer;
        PriceComparer priceComparer;
        ManufacturerComparer manufacturerComparer;

        public TitleComparer TitleComparer { get => titleComparer ?? (titleComparer = new TitleComparer()); }
        public PriceComparer PriceComparer { get => priceComparer ?? (priceComparer = new PriceComparer()); }
        public ManufacturerComparer ManufacturerComparer { get => manufacturerComparer ?? (manufacturerComparer = new ManufacturerComparer()); }
        public BatteryCapacityComparer BatteryCapacityComparer { get => batteryCapacityComparer ?? (batteryCapacityComparer = new BatteryCapacityComparer()); }

    }
}
