using System;
using System.Windows;
using DataManagment.Helpers;
using DataManagment.Repositories;
using Models;

namespace UserInterface
{
    public partial class MainWindow : Window
    {
        private readonly CsvDataParser _csvDataParser;
        private readonly CompanyRepository _companyRepository;
        private readonly WorkSettingRepository _workSettingRepository;

        public MainWindow()
        {
            InitializeComponent();
            var context = new DataManagment.AppContext();
            _csvDataParser = new CsvDataParser(context);
            _companyRepository = new CompanyRepository(context);
            _workSettingRepository = new WorkSettingRepository(context);
            LoadCompanies();
            LoadWorkSettings();
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FilterWindow filterWindow = new FilterWindow();
            filterWindow.Show();
        }

        private void LoadCompanies()
        {
            var companies = _companyRepository.Get();
            CompaniesListBox.ItemsSource = companies;
        }

        private void LoadWorkSettings()
        {
            var workSettings = _workSettingRepository.Get();
            WorkSettingsListBox.ItemsSource = workSettings;
        }

        protected override void OnClosed(EventArgs e)
        {
            _companyRepository.Dispose();
            _workSettingRepository.Dispose();
            base.OnClosed(e);
        }
    }
}
