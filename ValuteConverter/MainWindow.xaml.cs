using System.Windows;
using System.Windows.Controls;
using ValuteConverter.AppData;

namespace ValuteConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CourseService _courseService;
        public MainWindow()
        {
            InitializeComponent();
            _courseService = new CourseService(SellValuteCmb, BuyValuteCbm, SellAmountTb, BuyAmountTb, SellRatioTbl, BuyRatioTbl, UpdateDateTbl);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _courseService.LoadCource();
        }

        private void SellAmountTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_courseService != null)
            {
                _courseService.ConvertValute();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SellValuteCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_courseService != null) { _courseService.ConvertValute(); }
        }

        private void BuyValuteCbm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_courseService != null) { _courseService.ConvertValute(); }
        }

        private void BuyAmountTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_courseService != null) { _courseService.ConvertValute(); }
        }
    }
}
