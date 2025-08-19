using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WinUIExportSample
{
    public class ViewModel
    {
        public ObservableCollection<WaterConsumption> DailyWaterIntake { get; set; }
        public ViewModel()
        {
            DailyWaterIntake = new ObservableCollection<WaterConsumption>
            {
                new WaterConsumption("Monday", 2.5),
                new WaterConsumption("Tuesday", 3.0),
                new WaterConsumption("Wednesday", 2.8),
                new WaterConsumption("Thursday", 3.2),
                new WaterConsumption("Friday", 2.7),
                new WaterConsumption("Saturday", 3.5),
                new WaterConsumption("Sunday", 3.0)
           };
        }
    }

    public class WaterConsumption
    {
        public string Day { get; set; }
        public double Liters { get; set; }

        public WaterConsumption(string day, double liters)
        {
            Day = day;
            Liters = liters;
        }
    }
}
