using System;
using System.Windows;
using DataManagment.Helpers;
using DataManagment.Repositories;

namespace UserInterface
{
    public partial class MainWindow : Window
    {
        private readonly CsvDataParser _csvDataParser;

        public MainWindow()
        {
            InitializeComponent();
            var context = new DataManagment.AppContext();
            _csvDataParser = new CsvDataParser(context);
            _companyRepository = new CompanyRepository(context);
            LoadCompanies();
        }

        private void ParseCsvButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = FilePathTextBox.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter a valid file path.");
                return;
            }

            try
            {
                _csvDataParser.ParseAndSave(filePath);
                MessageBox.Show("CSV file parsed and data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JobManagementWindow jobManagementWindow = new JobManagementWindow();
            jobManagementWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmployeeManagementWindow employeeManagementWindow = new EmployeeManagementWindow();
            employeeManagementWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            JobAssignmentWindow jobAssignmentWindow = new JobAssignmentWindow();
            jobAssignmentWindow.Show();
        }

        private readonly CompanyRepository _companyRepository;

        private void LoadCompanies()
        {
            var companies = _companyRepository.Get();
            CompaniesListBox.ItemsSource = companies;
        }

        protected override void OnClosed(EventArgs e)
        {
            _companyRepository.Dispose();
            base.OnClosed(e);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FilterWindow filterWindow = new FilterWindow();
            filterWindow.Show();
        }
    }
}
