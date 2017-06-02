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
using Plugins_hierarchy.Class;
using Microsoft.Win32;
using PluginApi;


namespace Plugins_hierarchy {
    public partial class MainWindow : Window {
        public List<IPlugin> objectList { get; set; } = new List<IPlugin>();

        public MainWindow() {
            InitializeComponent();
            RefreshPluginList();
        }

        public void RefreshObjectList() {
            Items.ItemsSource = null;
            Items.ItemsSource = objectList;
        }

        public void RefreshPluginList() {
            Types.ItemsSource = null;
            Types.ItemsSource = Api.GetInstance().getPluginList();
        }

        private void BtnInstallPlugin_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true) {
                    Api.GetInstance().InstallPlugin(openFileDialog.FileName);
                }
                MessageBox.Show("Plugin is successfully installed");
                RefreshPluginList();
            } catch {
                MessageBox.Show("Plugin was installed or invalid");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            try {
                PagesFrame.Navigate(new Pages.Add(Types.SelectedIndex, this));
            } catch {
                MessageBox.Show("Please select item in combobox");
            }
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (Items.SelectedIndex >= 0) {
                PagesFrame.Navigate(new Pages.View(Items.SelectedIndex, this));
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            if (Items.SelectedIndex >= 0) {
                PagesFrame.Navigate(new Pages.Edit(Items.SelectedIndex, this));
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            if (Items.SelectedIndex >= 0) {
                objectList.RemoveAt(Items.SelectedIndex);
                RefreshObjectList();
            }
        }

        private void BtnSerialize_Click(object sender, RoutedEventArgs e) {
            Serializer.Serialize(objectList, "serialize.xml");
        }

        private void BtnDeserialize_Click(object sender, RoutedEventArgs e) {
            List<Type> typeList = new List<Type>();
            foreach (var item in Api.GetInstance().getPluginList()) {
                typeList.Add(item.objectType);
            }
            try {
                objectList = (List<IPlugin>) Serializer.Deserialize(typeList, "serialize.xml");
            } catch {
                MessageBox.Show("Invalid deserealize file (may be XmlToJsonPlugin was installed?!)");
            }
            RefreshObjectList();
        }
    }
}
