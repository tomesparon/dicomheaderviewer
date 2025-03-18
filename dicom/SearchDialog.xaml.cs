/*
 * Created by SharpDevelop.
 * User: Tom Esparon
 * Date: 18/03/2025
 * Time: 14:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;


namespace dicom
{
    public partial class SearchDialog : Window
    {
        public string SearchTerm { get; private set; }

        public SearchDialog()
        {
            InitializeComponent();
            // Focus the SearchTextBox and select all text when dialog is loaded
            SearchTextBox.Focus();
            SearchTextBox.SelectAll();

            // Handle Enter key press to trigger search
            SearchTextBox.KeyDown += SearchTextBox_KeyDown;
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If Enter key is pressed, trigger search
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(sender, e);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTerm = SearchTextBox.Text;
            DialogResult = true;  // Close dialog and return true to indicate a search was made
        }
    }
}
