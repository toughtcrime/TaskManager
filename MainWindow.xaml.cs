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
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process[] processes = Process.GetProcesses();
        private List<Processlist> processlist = new List<Processlist>();
        private List<Processlist> foundList = new List<Processlist>();
        public MainWindow()
        {
            InitializeComponent();
            processes = Process.GetProcesses();
            ProcessInfo.Foreground = new SolidColorBrush(Colors.Black);
            foreach (Process item in processes)
            {
                processlist.Add(new Processlist() { id = item.Id, name = item.ProcessName });
            }
            ProcessInfo.ItemsSource = processlist;
        }

        //TODO
        private void LoadProcesses()
        {
           
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var pid = (ProcessInfo.SelectedItem as Processlist).id;
            var process = Process.GetProcessById(pid);
            processlist.RemoveAt(ProcessInfo.SelectedIndex);
            ICollectionView view = CollectionViewSource.GetDefaultView(ProcessInfo.ItemsSource);
            view.Refresh();
            process.Kill();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void Search_KeyDown(object sender, TextChangedEventArgs e)
        {
            foundList.Clear();
            for (int i = 0; i < processlist.Count; i++)
            {      
                if(processlist[i].name.Contains(Search.Text) || processlist[i].id.ToString().Contains(Search.Text))
                {
                    foundList.Add(processlist[i]);
                }
            }

            ProcessInfo.ItemsSource = foundList;
            ICollectionView view = CollectionViewSource.GetDefaultView(ProcessInfo.ItemsSource);
            view.Refresh();
            

            if(Search.Text == "")
            {
                foundList.Clear();
                processlist.Clear();
                processes = Process.GetProcesses();
                foreach (Process item in processes)
                {
                    processlist.Add(new Processlist() { id = item.Id, name = item.ProcessName });
                }
                ProcessInfo.ItemsSource = processlist;
                view.Refresh();
            }
        }


        private bool WhenToClear()
        {
            string str1;
            string str2;

            for(int i = 0; i < Search.Text.Length; i++)
            {
                str1 = Search.Text;
                if(str1.Length < Search.Text.Length)
                {

                }

            }


            return false;
        }
    }

    public class Processlist
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
