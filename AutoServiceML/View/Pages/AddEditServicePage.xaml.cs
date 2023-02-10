using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoServiceML.Model;

namespace AutoServiceML.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        Core db = new Core();
        private Service _currentService = null;
        private byte[] _mainImageData;
        public AddEditServicePage()
        {
            InitializeComponent();
            if (_currentService == null)
            {
                var service = new Service
                {
                    Title = TBoxTitle.Text,
                    Cost = decimal.Parse(TBoxCost.Text),
                    DurationInSecond = int.Parse(TBoxDuration.Text) * 60,
                    Description = TBoxDescription.Textl,
                    Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text)? 0 : double.Parse(TBoxDiscount.Text) / 100,
                    MainImage = _mainImageData,
                };
                db.context.Service.Add(service);
                db.context.SaveChanges();
            }
        }
        private void BtnSelectImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageService.Sourse = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
        }
    }
}
