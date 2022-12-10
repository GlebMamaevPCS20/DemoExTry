using System;
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

namespace Trade
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Order _currentOrder = new Order();

        public AddEditPage()
        {
            InitializeComponent();
            DataContext = _currentOrder;
            ComboPickUpPoints.ItemsSource = TradeEntities.GetContext().OrderPickupPoints.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentOrder.OrderStatus))
                errors.AppendLine("Укажите статус заказа");
            //if (string.IsNullOrWhiteSpace(Convert.ToString(_currentOrder.OrderPickupPoint)))
            //    errors.AppendLine("Укажите пункт выдачи заказа");
            if (string.IsNullOrWhiteSpace(_currentOrder.OrderDeliveryDate.ToString()))
                errors.AppendLine("Укажите дату доставки");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentOrder.OrderID == 0)
                TradeEntities.GetContext().Order.Add(_currentOrder);
            try
            {
                TradeEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            

        }
    }
}
