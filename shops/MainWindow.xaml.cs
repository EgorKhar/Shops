using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.Entity;



namespace shops
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            Database1Entities1 de = new Database1Entities1();
            InitializeComponent();
            comboShop.Items.Add("Все");
            foreach (var item in de.Shop)
            {
                comboShop.Items.Add(item.Shop1);
            }

        }
        private void execute_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            bool month = monthRadio.IsChecked.Value;
            bool week = weekRadio.IsChecked.Value;
            Database1Entities1 de = new Database1Entities1();
            if (comboShop.SelectedItem == null)
            {
                MessageBox.Show("Не выбран магазин");
                error = true;
            }
            if(datePicker1.SelectedDate == null)
            {
                MessageBox.Show("Не выбрана начальная дата");
                error = true;
            }
            if ((datePicker2.SelectedDate == null) || (datePicker2.SelectedDate < datePicker1.SelectedDate))
            {
                MessageBox.Show("Конечная дата не выбрана или раньше начальной");
                error = true;
            }
            if (!error)
            {
                
                PrepareTable((DateTime)datePicker1.SelectedDate, (DateTime)datePicker2.SelectedDate, month);
               
            }
        }
        public void PrepareTable(DateTime start, DateTime end, bool mon_week)
        {
            int col_quan;
            DateTime current = new DateTime();
            DateTime temp = new DateTime();

            
            Database1Entities1 de = new Database1Entities1();
            dataGrid1.Columns.Clear();
            if (mon_week)
                col_quan = (end.Month - start.Month + 1);
            else
                col_quan = ((end- start).Days + 6) / 7;

            //dataGrid1.Columns.Add(new DataGridTextColumn { Header = "Покупатель"});
            List<string[]> table = new List<string[]>();
            string[] row = new string[col_quan + 2];
            row[0] = "Покупатель";
            current = start;
            for (int i = 1; i <= col_quan; i++)
            {
                if (mon_week)
                {
                    row[i] = current.ToShortDateString();
                    current = current.AddMonths(1);

                }
                else
                {
                    row[i] = current.ToShortDateString();
                    current = current.AddDays(7);
                }

            }
            row[col_quan+1] = "Итого";
            table.Add(row);

            // Filling table

            
            int j = 0;
            foreach (var item in de.Customer)
            {
                row = new string[col_quan + 2];
                current = start;
                row[0] = item.Name;
                for (int i = 1; i <= col_quan; i++)
                {
                    if (mon_week)
                    {
                        temp = current.AddMonths(1);
                        if (comboShop.SelectedItem.ToString() == "Все")
                        {
                            var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Customer.Name = @p0 and CashHeading.Date between @p1 and @p2", item.Name, current, temp).ToList();
                            row[i] = ((result[0] != null) ? result[0] : 0).ToString();
                        }
                        else
                        {
                            var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Customer.Name = @p0 and Shop.Shop = @p3 and CashHeading.Date between @p1 and @p2", item.Name, current, temp, comboShop.SelectedItem).ToList();
                             row[i] = ((result[0] != null) ? result[0] : 0).ToString();
                        }
                        current = current.AddMonths(1);

                    }
                    else
                    {
                        temp = current.AddDays(7);
                        if (comboShop.SelectedItem.ToString() == "Все")
                        {
                            var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Customer.Name = @p0 and CashHeading.Date between @p1 and @p2", item.Name, current, temp).ToList();
                            row[i] = ((result[0] != null) ? result[0] : 0).ToString();
                        }
                        else
                        {
                            var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Customer.Name = @p0 and Shop.Shop = @p3 and CashHeading.Date between @p1 and @p2", item.Name, current, temp, comboShop.SelectedItem).ToList();
                            row[i] = ((result[0] != null) ? result[0] : 0).ToString();
                        }
                        current = current.AddDays(7);
                    }
                    
                }
                if (comboShop.SelectedItem.ToString() == "Все")
                {
                    var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Customer.Name = @p0 and CashHeading.Date between @p1 and @p2", item.Name, start, end).ToList();
                    row[col_quan + 1] = ((result[0] != null) ? result[0] : 0).ToString();
                }
                else
                {
                    var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Customer.Name = @p0 and Shop.Shop = @p3 and CashHeading.Date between @p1 and @p2", item.Name, start, end, comboShop.SelectedItem).ToList();
                    row[col_quan + 1] = ((result[0] != null) ? result[0] : 0).ToString();
                }
                table.Add(row);
            }
            row = new string[col_quan + 2];
            row[0] = "Итого";
            current = start;
            for (int i = 1; i <= col_quan; i++)
            {
                if (mon_week)
                {
                    temp = current.AddMonths(1);
                    if (comboShop.SelectedItem.ToString() == "Все")
                    {
                        var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and CashHeading.Date between @p0 and @p1", current, temp).ToList();
                        row[i] = ((result[0] != null) ? result[0] : 0).ToString();
                    }
                    else
                    {
                        var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Shop.Shop = @p2 and CashHeading.Date between @p0 and @p1", current, temp, comboShop.SelectedItem).ToList();
                        row[i] = ((result[0] != null) ? result[0] : 0).ToString();
                    }
                    current = current.AddMonths(1);

                }
                else
                {
                    temp = current.AddDays(7);
                    if (comboShop.SelectedItem.ToString() == "Все")
                    {
                        var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and CashHeading.Date between @p0 and @p1", current, temp).ToList();
                        row[i] = ((result[0] != null) ? result[0] : 0).ToString();
                    }
                    else
                    {
                        var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Shop.Shop = @p2 and CashHeading.Date between @p0 and @p1", current, temp, comboShop.SelectedItem).ToList();
                        row[i] = ((result[0] != null) ? result[0] : 0).ToString();
                    }
                    current = current.AddDays(7);
                }

            }
            if (comboShop.SelectedItem.ToString() == "Все")
            {
                var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and CashHeading.Date between @p0 and @p1", start,end).ToList();
                row[col_quan + 1] = ((result[0] != null) ? result[0] : 0).ToString();
            }
            else
            {
                var result = de.Database.SqlQuery<int?>("select sum(Good.Price) from Good, Customer, Cash, CashHeading, Shop where Cash.Id_good = Good.Id and Cash.Id_cashheading = CashHeading.Id and CashHeading.Id_shop = Shop.Id and Shop.Shop = @p2 and CashHeading.Date between @p0 and @p1", start, end, comboShop.SelectedItem).ToList();
                row[col_quan + 1] = ((result[0] != null) ? result[0] : 0).ToString();
            }
            table.Add(row);

            // Displaying Table

            dataGrid1.IsReadOnly = true;
            DataTable dt = new DataTable();
            foreach (var item in row)
            {
                dt.Columns.Add();
            }
            foreach(var rows in table)
            {
                dt.LoadDataRow(rows, false);
            }
            dataGrid1.ItemsSource = dt.AsDataView();
            
        }
        
        
    }
}
