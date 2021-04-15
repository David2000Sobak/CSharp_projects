using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace Project2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Wardrobe> w = new List<Wardrobe>();
        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "Files Word |*.docx"
            };
            if (dialog.ShowDialog() == true)
            {
                Word.Application app = new Word.Application();
                object filename = dialog.FileName;
                app.Documents.Open(filename, ReadOnly: true);
                Word.Document document = app.ActiveDocument;
                Word.Table table = document.Tables[2];

                for (int i = 2; i <= table.Rows.Count; i++)
                {
                    w.Add(new Wardrobe(){ CanId = table.Cell(i, 2).Range.Text,
                        Number = table.Cell(i, 3).Range.Text,
                        Type = table.Cell(i, 4).Range.Text
                    });
                };


                document.Close();
                app.Quit();
            }

        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            WardrobeData.ItemsSource = null;
            WardrobeData.ItemsSource = w;
        }
    }
}
