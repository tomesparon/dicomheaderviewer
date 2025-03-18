/*
 * Created by SharpDevelop.
 * User: Tom Esparon
 * Date: 18/03/2025
 * Time: 13:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
namespace dicom
{
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
			
			// Handle Ctrl + F for searching
			KeyDown += MainWindow_KeyDown;
		}

		private void BtnOpen_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog {
				Filter = "DICOM Files (*.dcm)|*.dcm",
				Title = "Select a DICOM File"
			};

			if (ofd.ShowDialog() == true) {
				RunDcmdump(ofd.FileName);
			}
		}
		
		public void LoadDicomFile(string dicomFile)
		{
			RunDcmdump(dicomFile);
		}

		private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            // Specify the path for the file to be saved
            string filePath = "output.txt";
            
            try
            {
                // Write the content of the TextBox to the file
                File.WriteAllText(filePath, txtOutput.Text);
                MessageBox.Show("File saved successfully as output.txt!");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the file saving process
                MessageBox.Show("Error saving file: {ex.Message}");
            }
        }
		
		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			// Detect Ctrl + F
			if (e.Key == Key.F && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) {
				// Open search dialog or trigger search functionality
				ShowSearchDialog();
			}
		}

		private void ShowSearchDialog()
		{
			SearchDialog searchDialog = new SearchDialog();

			if (searchDialog.ShowDialog() == true) {  // Show dialog and wait for user input
				string searchTerm = searchDialog.SearchTerm;

				if (!string.IsNullOrEmpty(searchTerm)) {
					int index = txtOutput.Text.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase);
					if (index >= 0) {
						txtOutput.Select(index, searchTerm.Length);
						txtOutput.Focus();
						
						
					} else {
						MessageBox.Show("Text not found.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}
		
		private void RunDcmdump(string dicomFile)
		{
			try {
				string dcmdumpPath = DcmdumpHelper.ExtractDcmdump();

				ProcessStartInfo psi = new ProcessStartInfo {
					FileName = dcmdumpPath,
					Arguments = string.Format("\"{0}\"", dicomFile),
					RedirectStandardOutput = true,
					RedirectStandardError = true,  // Capture errors
					UseShellExecute = false,
					CreateNoWindow = true
				};

				using (Process process = new Process { StartInfo = psi }) {
					process.Start();
					string output = process.StandardOutput.ReadToEnd();
					string errorOutput = process.StandardError.ReadToEnd(); // Capture stderr

					if (!string.IsNullOrWhiteSpace(errorOutput)) {
						MessageBox.Show("Error: " + errorOutput, "DICOM Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}

					txtOutput.Text = output;
					process.WaitForExit();
				}
			} catch (Exception ex) {
				MessageBox.Show("Error running dcmdump: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}


	}
}
