using Excel = Microsoft.Office.Interop.Excel;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ExcelFileUnion
{
    public partial class MainWindow : Window
    {
        Dictionary<string, List<Worksheet>> excelData;
        List<string> filePaths = new List<string>();
        private const string file1 = @"C:\Users\galya\Desktop\Tablitsa_Ux-Rts_St_glubokaya_Per_Glubokaya_-_Andrianovskaya_ver1_24_03_2021.xlsx";
        private const string file2 = @"C:\Users\galya\Desktop\Tablitsa_Ux-Rts_St_glubokaya_Per_Podkamennaya-Glubokaya_ver1_23_03_2021.xlsx";
        private const string file3 = @"C:\Users\galya\Desktop\Tablitsa_Ux-Rts_Glubokaya_ver1_17_02_2021.xlsx";

        public MainWindow()
        {
            InitializeComponent();
            ElementSet();
        }

        private void ElementSet()
        {
            filePaths.Add(file1);
            filePaths.Add(file2);
            filePaths.Add(file3);

            progressBar.Maximum = filePaths.Count;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            excelData = new Dictionary<string, List<Worksheet>>();
            AllFilesReadAsync(filePaths);
        }

        private void ButtonPathChoose_Click(object sender, RoutedEventArgs e)
        {
            ChoosePath();
        }

        private void ChoosePath()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Excel files |*.xlsx"
            };
            if (dialog.ShowDialog() == true)
            {
                txbFileName.Text = dialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(excelData == null)
            {
                MessageBox.Show("Please, extract data from excel files.");
                return;
            }
            if (string.IsNullOrEmpty(txbFileName.Text))
            {
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    Filter = "Excel files |*.xlsx"
                };
                if (dialog.ShowDialog() == true)
                {
                    txbFileName.Text = dialog.FileName;
                }
                return;
            }

            btnGenerate.IsEnabled = false;

            UploadData();

            btnGenerate.IsEnabled = true;
        }

        private void UploadData()
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            if (app == null)
            {
                MessageBox.Show("Excel is not installed properly!", "Error");
                return;
            }
            Excel.Workbook wb = app.Workbooks.Add();
            foreach (var item in excelData)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    wb.Worksheets.Add(After: wb.Worksheets[wb.Worksheets.Count]);
                    Excel.Worksheet sheet = wb.Worksheets[wb.Worksheets.Count];
                    sheet.Name = $"{item.Key}({i + 1})";
                    sheet.Cells[3, 1] = item.Value[i].Headers.TableName;
                    sheet.Cells[4, 1] = item.Value[i].Headers.Station;
                    sheet.Cells[5, 1] = item.Value[i].Headers.StationSpan;
                    sheet.Range["A3:A5"].Font.Bold = true;
                    sheet.Range["A3:A5"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    for (int r = 0; r < 3; r++)
                    {
                        sheet.Range[$"A{r + 3}:B{r + 3}"].Merge();
                    }


                    int row = 9;
                    for (int j = 0; j < item.Value[i].Data.Count; j++)
                    {
                        sheet.Cells[row, 1] = item.Value[i].Data[j].Name;
                        sheet.Range[$"A{row}:B{row}"].Merge();
                        sheet.Cells[row, 1].Font.Bold = true;
                        sheet.Cells[row, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        sheet.Cells[row + 1, 1] = "Номер канала в приборе";
                        sheet.Cells[row + 1, 2] = "Номер  оборудования";
                        for (int k = 0; k < 36; k++)
                        {
                            sheet.Cells[row + 2 + k, 1] = item.Value[i].Data[j].Span[k].ChannelNumber;
                            sheet.Cells[row + 2 + k, 2] = item.Value[i].Data[j].Span[k].Number;
                        }
                        row += 38;
                    }
                    sheet.Columns.AutoFit();
                    sheet.UsedRange.Font.Name = "Times New Roman";
                    sheet.UsedRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    sheet.Range["A3:B8"].Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;
                }
            }
            wb.Worksheets[1].Delete();



            try
            {
                wb.SaveAs(txbFileName.Text);
            }
            catch (System.Exception)
            {
                MessageBox.Show("File path is invalid, please, try again.");
            }
            
            
        }

        private async void AllFilesReadAsync(List<string> filePaths)
        {
            btnExtract.IsEnabled = false;
            btnGenerate.IsEnabled = false;

            tbFileIndicator.Text = $"Files completed: 0 out of {filePaths.Count} (0%)";
            foreach (var item in filePaths)
            {
                FileReadAsync(item);
                await Task.Delay(2000);
            }
            await Task.Delay(1000);
            progressBar.Value = 0;
            tbFileIndicator.Text = "";

            btnExtract.IsEnabled = true;
            btnGenerate.IsEnabled = true;
        }
        private async void FileReadAsync(string fileName)
        {

            await Task.Run(() =>
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Open(fileName, ReadOnly: true);
                foreach (Excel.Worksheet ws in wb.Worksheets)
                {
                    string name = ws.Name;
                    if (!excelData.Any(x => x.Key == ws.Name))
                        excelData[name] = new List<Worksheet>();

                    ws.Activate();
                    var headers = ws.Range["A3:A5"].Value;
                    excelData[name].Add(new Worksheet
                    {
                        Headers = new WhHeader
                        {
                            TableName = headers[1, 1],
                            Station = headers[2, 1],
                            StationSpan = headers[3, 1]
                        },
                        Data = new List<ChannelSpan>()

                    });
                    for (int i = 0; i < 6; i++)
                    {
                        int row = 9;
                        excelData[name][excelData[name].Count - 1].Data.Add(new ChannelSpan
                        {
                            Name = ws.Range[$"A{row}"].Value,
                            Span = new List<Equipment>()
                        });
                        var range = ws.Range[$"B{row + 2}:C{row + 37}"].Value;
                        for (int j = 1; j <= 36; j++)
                        {
                            excelData[name][excelData[name].Count - 1].Data[i].Span.Add(new Equipment
                            {
                                ChannelNumber = range[j, 1],
                                Number = range[j, 2]
                            });
                        }
                        row += 38;
                    }

                }
                wb.Close();
                app.Quit();
            });
            progressBar.Value++;
            tbFileIndicator.Text = $"Files completed: {progressBar.Value} out of {filePaths.Count} ({(progressBar.Value/filePaths.Count).ToString("#.##%")})";
        } 
    }
}
