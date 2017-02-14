using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace evidence_osob_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> itemsFromDb;
        Person item = new Person();
        int p;
        int g;
        public MainWindow()
        {
            InitializeComponent();
            hidden();
            entry.Visibility = Visibility.Visible;
            ToDoItemsListView.Visibility = Visibility.Visible;

            itemsFromDb = new ObservableCollection<Person>( Data.GetItemsNotDoneAsync().Result);

            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");

            Debug.WriteLine(itemsFromDb.Count);
            foreach (Person todoItem in itemsFromDb)
            {
                Debug.WriteLine(todoItem);
            }

            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");
          
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }

        private void entry_Click(object sender, RoutedEventArgs e)
        {
            hidden();

            t1.Visibility = Visibility.Visible;
            t2.Visibility = Visibility.Visible;
            t3.Visibility = Visibility.Visible;
            t4.Visibility = Visibility.Visible;
            comboBox.Visibility = Visibility.Visible;
            l1.Visibility = Visibility.Visible;
            l2.Visibility = Visibility.Visible;
            l3.Visibility = Visibility.Visible;
            l5.Visibility = Visibility.Visible;
            news.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Visible;
            
        }

        private static database _data;
        public static database Data
        {
            get
            {
                if (_data == null)
                {
                    var fileHelper = new FileHelper();
                    _data = new database(fileHelper.GetLocalFilePath("PersonSQLite.db3"));
                }
                return _data;
            }
        }
        public void hidden()
        {
            entry.Visibility = Visibility.Hidden;
            ToDoItemsListView.Visibility = Visibility.Hidden;
            t1.Visibility = Visibility.Hidden;
            t2.Visibility = Visibility.Hidden;
            t3.Visibility = Visibility.Hidden;
            t4.Visibility = Visibility.Hidden;
            comboBox.Visibility = Visibility.Hidden;
            l1.Visibility = Visibility.Hidden;
            l2.Visibility = Visibility.Hidden;
            l3.Visibility = Visibility.Hidden;
            l5.Visibility = Visibility.Hidden;
            news.Visibility = Visibility.Hidden;
            edit.Visibility = Visibility.Hidden;
            del.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Hidden;
            er.Visibility = Visibility.Hidden;
        }

        private void news_Click(object sender, RoutedEventArgs e)
        {
            item.jmeno = t1.Text;
            item.prijmeni = t2.Text;
            if(t3.Text.Length == 6)
            {
                item.r_num = t3.Text;
                if (t4.Text.Length == 4)
                {
                   item.r_num2 = t4.Text;
                    if (c1.IsSelected == true)
                    {
                        item.pohlavi = 1;
                    }
                    if (c2.IsSelected == true)
                    {
                        item.pohlavi = 2;
                    }

                    Data.SaveItemAsync(item);
                    itemsFromDb.Add(item);
                    hidden();
                    entry.Visibility = Visibility.Visible;
                    ToDoItemsListView.Visibility = Visibility.Visible;
                    
                    t1.Text = "";
                    t2.Text = "";
                    t3.Text = "";
                    t4.Text = "";
                } else
                {
                    er.Visibility = Visibility.Visible;
                }
            } else
            {
                er.Visibility = Visibility.Visible;
            }
                                
        }
        private void ToDoItemsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
       {          
            if (ToDoItemsListView.SelectedItems.Count > 0)
            {
           
                Person todoItem = (Person)ToDoItemsListView.SelectedItems[0];
                g = itemsFromDb.IndexOf(todoItem);               
                t1.Text = todoItem.jmeno;
                t2.Text = todoItem.prijmeni;
                t3.Text = todoItem.r_num;
                t4.Text = todoItem.r_num2;
              if(todoItem.pohlavi == 1)
                {
                    c1.IsSelected = true;
                }
                if (todoItem.pohlavi == 2)
                {
                    c2.IsSelected = true;
                }

                p = todoItem.ID;               
            }
            hidden();

            t1.Visibility = Visibility.Visible;
            t2.Visibility = Visibility.Visible;
            t3.Visibility = Visibility.Visible;
            t4.Visibility = Visibility.Visible;
            comboBox.Visibility = Visibility.Visible;
            l1.Visibility = Visibility.Visible;
            l2.Visibility = Visibility.Visible;
            l3.Visibility = Visibility.Visible;
            l5.Visibility = Visibility.Visible;
            edit.Visibility = Visibility.Visible;
            del.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Visible;
        }

        private void edit_click(object sender, RoutedEventArgs e)
        {


            item.jmeno = t1.Text;
            item.prijmeni = t2.Text;
            if (t3.Text.Length == 6)
            {
                item.r_num = t3.Text;
                if (t4.Text.Length == 4)
                {
                    item.r_num2 = t4.Text;
                    if (c1.IsSelected == true)
                    {
                        item.pohlavi = 1;
                    }
                    if (c2.IsSelected == true)
                    {
                        item.pohlavi = 2;
                    }

                    itemsFromDb.RemoveAt(g);
                    Data.SaveItemAsync(item);
                    itemsFromDb.Add(item);
                    hidden();
                    entry.Visibility = Visibility.Visible;
                    ToDoItemsListView.Visibility = Visibility.Visible;
                    t1.Text = "";
                    t2.Text = "";
                    t3.Text = "";
                    t4.Text = "";
                }
                else
                {
                    er.Visibility = Visibility.Visible;
                }
            }
            else
            {
                er.Visibility = Visibility.Visible;
            }                    
        }

        private void del_click(object sender, RoutedEventArgs e)
        {
            Data.DeleteItemAsync(p);
            itemsFromDb.RemoveAt(g);
            
            hidden();
            entry.Visibility = Visibility.Visible;
            ToDoItemsListView.Visibility = Visibility.Visible;

            t1.Text = "";
            t2.Text = "";
            t3.Text = "";
            t4.Text = "";
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            hidden();

            entry.Visibility = Visibility.Visible;
            ToDoItemsListView.Visibility = Visibility.Visible;

            t1.Text = "";
            t2.Text = "";
            t3.Text = "";
            t4.Text = "";            
        }

        private void pre(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void prf(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Ža-ž]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
