using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace arsing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dtTimePicker.MinDate = new DateTime(2019, 1, 1);
            dtTimePicker.MaxDate = DateTime.Now.Date;

            GetNameCurrency();


            
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string date = Regex.Replace(dtTimePicker.Value.Date.ToString("dd/MM/yyyy"), "[.]", "/");
            GetValueAsync(cmbBoxCurrency.SelectedItem.ToString(), date);
            //Функция отвечает за вывод данных по стоимости валюты в рублях на выбранную дату,
            // если даныые по данной валюте имеются на сайте.

            ndLabel.Text = "";
            ctnChart.AxisX.Clear();
            string currencyCode = GetCurrencyCode(cmbBoxCurrency.SelectedItem.ToString());
            List<GraphData> data = GetGraphData(currencyCode, date);
            GraphBuilding(data);
            //Функции выше отвечают за вывод сбор и вывод данных по стоимости выбранной валюты на промежутке 2-ух последних лет
            //и визуализации их ввиде графика.

        }

       
        private void GetNameCurrency()
        {
            btnSearch.Enabled = false;

            string line = "";
            using (WebClient wc = new WebClient())
                line = wc.DownloadString("http://www.cbr.ru/scripts/XML_val.asp?d=0");
            MatchCollection matches = Regex.Matches(line, "<Name>(.*?)</Name>");
            string[] items = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                items[i] = matches[i].Groups[1].Value;
            }
            cmbBoxCurrency.Items.AddRange(items);
            cmbBoxCurrency.SelectedIndex = 0;

            btnSearch.Enabled = true;
        }

        private async void GetValueAsync(string currencyName, string date)
        {
            string result = "";
            await Task.Run(() =>
            {
               string line = "";
               using (WebClient wc = new WebClient())
                   line = wc.DownloadString($"http://www.cbr.ru/scripts/XML_daily.asp?date_req={date}");
               Match match = Regex.Match(line, @"<Name>" + currencyName + @"</Name><Value>(.*?)</Value>");
               result = match.Groups[1].Value;
            });
            if (string.IsNullOrEmpty(result))
                valueLabel.Text = "No data on the site";
            else
                valueLabel.Text = result + " Rub.";
        }


        private string GetCurrencyCode(string currencyName)
        {
            string line = "";
            using (WebClient wc = new WebClient())
                line = wc.DownloadString("http://www.cbr.ru/scripts/XML_val.asp?d=0");
            Match match = Regex.Match(line, @"<Name>" + currencyName + @"</Name>(.*?)<ParentCode>(.*?) </ParentCode>");
            return match.Groups[2].Value.ToString();
        }

        private List<GraphData> GetGraphData(string currencyCode, string lastDate)
        {
            string line = "";
            using (WebClient wc = new WebClient())
                line = wc.DownloadString($"http://www.cbr.ru/scripts/XML_dynamic.asp?date_req1=01/01/2019&date_req2={lastDate}&VAL_NM_RQ={currencyCode}");
            MatchCollection matches = Regex.Matches(line, "<Record Date=\"0(.*?)\" Id=(.*?)<Value>(.*?)</Value>");

            var dataList = new List<GraphData>();
            foreach (Match match in matches)
            {
                dataList.Add(new GraphData { Value = Convert.ToDouble(match.Groups[3].Value), Date = match.Groups[1].Value});
            }

            return dataList;
        }

        private void GraphBuilding(List<GraphData> data)
        {
            SeriesCollection series = new SeriesCollection();
            ChartValues<double> cv = new ChartValues<double>();
            List<string> dates = new List<string>();
            foreach (GraphData item in data)
            {
                cv.Add(item.Value);
                dates.Add(item.Date);
            }

            ctnChart.AxisX.Add(new Axis { Title = "Date", Labels = dates });

            LineSeries line = new LineSeries();
            line.Title = "Rub";
            line.Values = cv;
            series.Add(line);
            ctnChart.Series = series;
            if (dates.Count == 0)
                ndLabel.Text = "No data";
        }
    }
}
