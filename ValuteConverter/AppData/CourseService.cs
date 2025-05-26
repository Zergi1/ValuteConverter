using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ValuteConverter.AppData
{
    public class CourseService
    {
        private ComboBox _sellValuteComboBox;
        private ComboBox _buyValuteComboBox;
        private TextBox _sellValuteTextBox;
        private TextBox _buyValuteTextBox;
        private TextBlock _sellRatioTextBlock;
        private TextBlock _buyRatioTextBlock;
        private TextBlock _updateDateTextBlock;

        public CourseService(ComboBox sellValuteComboBox, ComboBox buyValuteComboBox, TextBox sellValuteTextBox, TextBox buyValuteTextBox, TextBlock sellRatioTextBlock, TextBlock buyRatioTextBlock, TextBlock updateDateTextBlock)
        {
            _sellValuteComboBox = sellValuteComboBox;
            _buyValuteComboBox = buyValuteComboBox;
            _sellValuteTextBox = sellValuteTextBox;
            _buyValuteTextBox = buyValuteTextBox;
            _sellRatioTextBlock = sellRatioTextBlock;
            _buyRatioTextBlock = buyRatioTextBlock;
            _updateDateTextBlock = updateDateTextBlock;
        }

        public string RequestUrl { get; private set; } = "https://www.cbr-xml-daily.ru/daily_json.js";
        public string LocalRequestUrl { get; private set; } = "\\\\fs\\Profiles$\\Students\\кИС-33\\Набойщиков.Алексей\\Desktop\\сылка на api";

        public Valute SellValute { get; private set; }
        public Valute BuyValute { get; private set; }
        public double SellAmount { get; private set; }
        public double BuyAmount { get; private set; }
        public double SellRatio { get; private set; }
        public double BuyRatio { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public Cource Course { get; private set; }
        public List<Valute> Valutes { get; private set; }

        public async Task LoadCource()
        {
            try
            {
                // 1. Создаем HTTP-клиент для отправки запроса на веб-сервер
                HttpClient httpClient = new HttpClient();

                // 2. Создаем переменную для хранения ответа
                var response = await httpClient.GetStringAsync(RequestUrl);

                // 3. Проводим десереализация ответа (из строки делаем объекты)
                // 3.1 Устанавливаем пакет Newtonsoft.Json
                // 3.2 Проверяем переменную response на наличие значения
                // 3.3 Вызываем метод DeserializeOblect() с указанием типа данных объекта 

                if (!string.IsNullOrEmpty(response))
                {
                    // Получаем курс валют
                    Course = JsonConvert.DeserializeObject<Cource>(response);

                    // Получаем списки валют из курса


                    Valutes = Course.Valute.Values.ToList();
                    //Добавляем новую валюту
                    Valute rouble = new Valute
                    {
                        ID = "R000001",
                        NumCode = "643",
                        CharCode = "RUB",
                        Nominal = 1,
                        Name = "Российский рубль",
                        Value = 1,
                        Previous = 1
                    };

                    //Вставляем новую валюту
                    Valutes.Insert(0, rouble);

                    // Загружаем список валют в выпадающие списки
                    _sellValuteComboBox.ItemsSource = Valutes;
                    _buyValuteComboBox.ItemsSource = Valutes;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void ConvertValute()
        {
            SellValute = _sellValuteComboBox.SelectedItem as Valute;
            BuyValute = _buyValuteComboBox.SelectedItem as Valute;
            SellAmount = Convert.ToDouble(_sellValuteTextBox.Text);
            BuyAmount = SellAmount * BuyValute.Nominal / SellValute.Nominal * SellValute.Value / BuyValute.Value;
            _buyValuteTextBox.Text = $"{BuyAmount:F2}";


        }

        public void CalculateRatio()
        {
            SellRatio = BuyAmount / SellAmount;
            BuyRatio = SellAmount / BuyAmount;

            _sellRatioTextBlock.Text = $"{SellValute.Nominal} {SellValute.CharCode} ={BuyRatio:F4} {BuyValute.CharCode}";
            _buyRatioTextBlock.Text = $"{BuyValute.Nominal} {BuyValute.CharCode} = {SellRatio:F4} {SellValute.CharCode}";
        }
    }
}
