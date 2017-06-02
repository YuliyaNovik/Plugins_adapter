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
using System.Reflection;
using Plugins_hierarchy.Class;
using PluginApi;


namespace Plugins_hierarchy.Pages {
    public partial class Add : Page {
        private Plugin plugin;
        private int index;
        private List<TextBox> inputField = new List<TextBox>();
        private List<TextBox> inputProperty = new List<TextBox>();
        private MainWindow super = new MainWindow();

        public Add(int index, MainWindow super) {
            InitializeComponent();
            this.plugin = Api.GetInstance().getPluginList()[index];
            this.super = super;
            this.index = index;
            CreateMarkup();
        }

        private void CreateMarkup() {
            PluginApi.IPlugin obj = (PluginApi.IPlugin)this.plugin.CreateObject();
            List<string> fieldsInput = obj.getFields();
            List<string> propsInput = obj.getProperties();

            Grid dynamicGrid = CreateGrid(fieldsInput, propsInput);
            CreateInput(dynamicGrid, fieldsInput, propsInput);
        }

        private Grid CreateGrid(List<string> fieldsInput, List<string> propsInput) {
            Grid dynamicGrid = new Grid();

            List<RowDefinition> rows = new List<RowDefinition>();
            foreach (var item in fieldsInput) {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(30);
                rows.Add(row);
            }
            foreach (var item in propsInput) {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(30);
                rows.Add(row);
            }
            RowDefinition rowBtn = new RowDefinition();
            rowBtn.Height = new GridLength(30);
            rows.Add(rowBtn);

            ColumnDefinition colInput = new ColumnDefinition();
            ColumnDefinition colDescription = new ColumnDefinition();
            colDescription.Width = new GridLength(100);

            dynamicGrid.ColumnDefinitions.Add(colDescription);
            dynamicGrid.ColumnDefinitions.Add(colInput);
            foreach (var item in rows) {
                dynamicGrid.RowDefinitions.Add(item);
            }

            return dynamicGrid;
        }

        private TextBox CreateInputRow(string stringInput, int pos, Grid dynamicGrid) {
            TextBlock label = new TextBlock();
            label.Text = stringInput;
            label.Margin = new Thickness(5, 5, 5, 5);
            Grid.SetRow(label, pos);
            Grid.SetColumn(label, 0);
            dynamicGrid.Children.Add(label);

            TextBox inputBox = new TextBox();
            inputBox.Margin = new Thickness(5, 5, 5, 5);
            Grid.SetRow(inputBox, pos);
            Grid.SetColumn(inputBox, 1);
            dynamicGrid.Children.Add(inputBox);
            return inputBox;
        }

        private void CreateInput(Grid dynamicGrid, List<string> fieldsInput, List<string> propsInput) {
            int index = 0;
            for (int i = 0; i < fieldsInput.Count; i++) {
                TextBox inputBox = CreateInputRow(fieldsInput[i], index, dynamicGrid);
                inputField.Add(inputBox);
                index++;
            }

            for (int i = 0; i < propsInput.Count; i++) {
                TextBox inputBox = CreateInputRow(propsInput[i], index, dynamicGrid);
                inputProperty.Add(inputBox);
                index++;
            }

            Button btnAdd = new Button();
            btnAdd.Content = "Add";
            btnAdd.Margin = new Thickness(5, 5, 5, 5);

            btnAdd.Click += BtnAdd_Click;
            Grid.SetRow(btnAdd, fieldsInput.Count + propsInput.Count);
            Grid.SetColumn(btnAdd, 1);
            dynamicGrid.Children.Add(btnAdd);

            this.Content = dynamicGrid;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            PluginApi.IPlugin obj = (PluginApi.IPlugin) this.plugin.CreateObject();
            List<string> field = obj.getFields();
            List<string> property = obj.getProperties();

            try {
                for (int i = 0; i < field.Count; i++) {
                    if (inputField[i].Text == "") {
                        MessageBox.Show("Invalid input");
                        return;
                    }

                    if (this.plugin.objectType.GetField(field[i]).FieldType == typeof(double)) {
                        this.plugin.objectType.GetField(field[i]).SetValue(obj, Double.Parse(inputField[i].Text));
                    } else if (this.plugin.objectType.GetField(field[i]).FieldType == typeof(int)) {
                        this.plugin.objectType.GetField(field[i]).SetValue(obj, Int32.Parse(inputField[i].Text));
                    } else {
                        this.plugin.objectType.GetField(field[i]).SetValue(obj, inputField[i].Text);
                    }
                }
                for (int i = 0; i < property.Count; i++) {
                    if (inputProperty[i].Text == "") {
                        MessageBox.Show("Invalid input");
                        return;
                    }

                    if (this.plugin.objectType.GetProperty(property[i]).PropertyType == typeof(double)) {
                        this.plugin.objectType.GetProperty(property[i]).SetValue(obj, Double.Parse(inputProperty[i].Text));
                    } else if (this.plugin.objectType.GetProperty(property[i]).PropertyType == typeof(int)) {
                        this.plugin.objectType.GetProperty(property[i]).SetValue(obj, Int32.Parse(inputProperty[i].Text));
                    } else {
                        this.plugin.objectType.GetProperty(property[i]).SetValue(obj, inputProperty[i].Text);
                    }
                }
            } catch {
                MessageBox.Show("Invalid input");
                return;
            }


            super.objectList.Add(obj);
            super.RefreshObjectList();
        }
    }
}
